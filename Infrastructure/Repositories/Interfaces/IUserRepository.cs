using Application.DTOs.Auth;
using Application.DTOs.User;
using Domain.Models;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        Task<bool> IsUniqueUserName(string username);

        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);

        Task<UserDTO> Register(RegisterRequestDTO registerRequestDTO);

        Task<ApplicationUser> GetUserByID(string userID);
        
        Task<bool> UpdateAsync(ApplicationUser user);
    }
}