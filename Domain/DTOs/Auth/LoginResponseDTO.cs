using Application.DTOs.User;

namespace Application.DTOs.Auth
{
    public class LoginResponseDTO
    {
        public UserDTO User { get; set; }

        public string Token { get; set; }
    }
}
