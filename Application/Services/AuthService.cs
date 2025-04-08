using Application.DTOs.Auth;
using Application.Interfaces;
using System.ComponentModel.DataAnnotations;
using Infrastructure.Repositories.Interfaces;
using Application.DTOs.User;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<LoginResponseDTO> LoginAsync(LoginRequestDTO loginRequestDTO)
        {
            return await _userRepository.Login(loginRequestDTO);
        }

        public async Task<UserDTO> RegisterAsync(RegisterRequestDTO registerRequestDTO)
        {
            var emailExist = await _userRepository.GetAsync(user => user.Email == registerRequestDTO.Email);
            if (emailExist != null)
            {
                throw new ValidationException("Email Already exists");
            }

            return await _userRepository.Register(registerRequestDTO);
        }
    }
}
