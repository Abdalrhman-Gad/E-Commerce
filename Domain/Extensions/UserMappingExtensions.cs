using Application.DTOs.Auth;
using Application.DTOs.User;
using Domain.Models;

namespace Application.Extensions
{
    public static class UserMappingExtensions
    {
        public static UserDTO ToUserDTO(this ApplicationUser user)
        {
            if (user == null)
            {
                return null;
            }

            return new UserDTO
            {
                UserName = user.UserName,
                Name = user.Name,
                Email = user.Email,
                ErrorMessages = []
            };
        }

        public static ApplicationUser ToUser(this UserDTO userDto)
        {
            if (userDto == null)
            {
                return null;
            }

            return new ApplicationUser
            {
                UserName = userDto.UserName,
                Name = userDto.Name,
                Email = userDto.Email,
                NormalizedEmail = userDto.Email.ToUpper(),
                NormalizedUserName = userDto.UserName.ToUpper(),
            };
        }

        public static ApplicationUser ToUser(this RegisterRequestDTO registerRequestDTO)
        {
            if (registerRequestDTO == null)
            {
                return null;
            }

            return new ApplicationUser
            {
                UserName = registerRequestDTO.UserName,
                Name = registerRequestDTO.Name,
                Email = registerRequestDTO.Email,
                NormalizedEmail = registerRequestDTO.Email.ToUpper(),
            };
        }
    }
}