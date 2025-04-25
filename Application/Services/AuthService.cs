using Domain.DTOs.Auth;
using Infrastructure.Repositories.Interfaces;
using Application.DTOs.User;
using Application.Interfaces;
using Domain.Exceptions.User;
using Application.DTOs.Auth;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> ConfirmEmailAsync(string userId, string token)
        {
            var isConfirmed = await _userRepository.ConfirmEmailAsync(userId, token);

            if (isConfirmed)
                return true;

            throw new UserNotActivatedException(userId);  // Custom exception for email confirmation failure
        }

        public async Task<string> LoginAsync(LoginRequestDTO loginRequestDTO)
        {
            var token = await _userRepository.Login(loginRequestDTO);

            if (string.IsNullOrEmpty(token))
            {
                throw new InvalidUserCredentialsException();  // Custom exception for invalid credentials
            }

            return token;
        }

        public async Task<string> RegisterAsync(RegisterRequestDTO registerRequestDTO)
        {
            // Check if email already exists using custom exception for better clarity
            var emailExist = await _userRepository.GetAsync(user => user.Email == registerRequestDTO.Email);
            if (emailExist != null)
            {
                throw new EmailAlreadyExistsException(registerRequestDTO.Email);  // Custom exception for email existence
            }

            var registeredUser = await _userRepository.Register(registerRequestDTO);

            return "Registration successful! Please check your email for confirmation.";
        }
    }
}
