using Domain.DTOs.Image;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<bool> UploadUserImageAsync(string userId, ImageUploadRequestDTO request);
    }
}