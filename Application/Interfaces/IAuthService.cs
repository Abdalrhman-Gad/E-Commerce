using Application.DTOs.Auth;
using Application.DTOs.User;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> LoginAsync(LoginRequestDTO loginRequestDTO);
        
        Task<string> RegisterAsync(RegisterRequestDTO registerRequestDTO);
        
        Task<bool> ConfirmEmailAsync(string userId, string token);
    }
}