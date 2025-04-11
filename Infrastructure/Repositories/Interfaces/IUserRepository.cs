using Application.DTOs.Auth;
using Application.DTOs.User;
using Domain.Models;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        Task<bool> IsUniqueUserName(string username);

        Task<string> Login(LoginRequestDTO user);

        Task<ApplicationUser> Register(RegisterRequestDTO user);

        Task<ApplicationUser> GetUserByID(string userID);

        Task<bool> UpdateImageAsync(string userId, int imageId);

        Task<bool> ConfirmEmailAsync(string userId, string token);
    }
}