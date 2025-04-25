using Application.DTOs.Auth;
using Domain.DTOs.Auth;
using Domain.Exceptions.User;
using Domain.Models;
using Infrastructure.Persistence;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Repositories.Services
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;
        private readonly string _securityKey;

        public UserRepository(
            ApplicationDbContext db,
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IEmailSender emailSender
        ) : base(db)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
            _configuration = configuration;
            _securityKey = configuration.GetValue<string>("ApiSettings:Secret")
                ?? throw new InvalidOperationException("ApiSettings:Secret is not configured.");
        }

        public async Task<ApplicationUser> GetUserByID(string userId)
        {
            var user = await _db.ApplicationUsers.FindAsync(userId);
            return user ?? throw new UserNotFoundException(userId);
        }

        public async Task<bool> IsUniqueUserName(string username)
        {
            return await _userManager.FindByNameAsync(username) == null;
        }

        public async Task<string> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginRequestDTO.Email);
            await ValidateUserCredentials(user, loginRequestDTO.Password);

            return GenerateJwtToken(user, await _userManager.GetRolesAsync(user));
        }

        public async Task<ApplicationUser> Register(RegisterRequestDTO registerRequestDTO)
        {
            var user = new ApplicationUser
            {
                UserName = registerRequestDTO.UserName,
                Email = registerRequestDTO.Email,
                Name = registerRequestDTO.Name,
            };

            ValidateEmailFormat(registerRequestDTO.Email);
            await ValidateRolesExistence(registerRequestDTO.Roles);

            var result = await CreateUserAsync(user, registerRequestDTO.Password);
            if (!result.Succeeded) HandleUserCreationErrors(result.Errors, registerRequestDTO);

            await AssignRolesAsync(user, registerRequestDTO.Roles);
            await SendConfirmationEmailAsync(user);

            return user;
        }

        public async Task<bool> UpdateImageAsync(string userId, int imageId)
        {
            var existingUser = await _db.ApplicationUsers.FindAsync(userId)
                ?? throw new UserNotFoundException(userId);

            existingUser.ImageId = imageId;
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId)
                ?? throw new UserNotFoundException(userId);

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                user.EmailConfirmed = true;
                await _userManager.UpdateAsync(user);
                return true;
            }

            return false;
        }

        #region Private Methods

        private void ValidateEmailFormat(string email)
        {
            var emailAddress = new EmailAddressAttribute();
            if (!emailAddress.IsValid(email))
            {
                throw new InvalidEmailFormatException(email);
            }
        }

        private async Task ValidateRolesExistence(IEnumerable<string> roles)
        {
            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    throw new InvalidRoleException(role);
                }
            }
        }

        private async Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        private async Task SendConfirmationEmailAsync(ApplicationUser user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = GenerateEmailConfirmationLink(user.Id, token);

            var confirmationEmailSubject = _configuration["EmailSettings:ConfirmationEmailSubject"] ?? "E-Commerce Confirmation Link";

            await _emailSender.SendEmailAsync(user.Email, confirmationEmailSubject,
                $"Please confirm your account by clicking <a href='{confirmationLink}'>here</a>");
        }

        private string GenerateJwtToken(ApplicationUser user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_securityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task AssignRolesAsync(ApplicationUser user, IEnumerable<string> roles)
        {
            if (roles == null || !roles.Any()) return;

            foreach (var role in roles)
            {
                await _userManager.AddToRoleAsync(user, role);
            }
        }

        private static string GenerateEmailConfirmationLink(string userId, string token)
        {
            return $"https://localhost:44382/api/AuthUser/confirm-email?userId={userId}&token={Uri.EscapeDataString(token)}";
        }

        private async Task ValidateUserCredentials(ApplicationUser user, string password)
        {
            if (user == null || !await _userManager.CheckPasswordAsync(user, password))
            {
                throw new InvalidUserCredentialsException();
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                throw new UserNotActivatedException(user.Email);
            }
        }


        private void HandleUserCreationErrors(IEnumerable<IdentityError> errors, RegisterRequestDTO registerRequestDTO)
        {
            foreach (var error in errors)
            {
                switch (error.Code)
                {
                    case "DuplicateUserName":
                        throw new UsernameAlreadyExistsException(registerRequestDTO.UserName);
                    case "DuplicateEmail":
                        throw new EmailAlreadyExistsException(registerRequestDTO.Email);
                    case "PasswordTooShort":
                        throw new PasswordTooWeakException("at least 6 characters");
                    case "PasswordRequiresDigit":
                        throw new PasswordTooWeakException("at least one digit");
                    case "PasswordRequiresLower":
                        throw new PasswordTooWeakException("at least one lowercase letter");
                    case "PasswordRequiresUpper":
                        throw new PasswordTooWeakException("at least one uppercase letter");
                    case "PasswordRequiresNonAlphanumeric":
                        throw new PasswordTooWeakException("at least one non-alphanumeric character");
                    default:
                        throw new UserRegistrationException();
                }
            }
        }

        #endregion
    }
}
