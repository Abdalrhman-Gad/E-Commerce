using Application.DTOs.Image;
using Domain.Models;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<bool> UploadUserImageAsync(string userId, ImageUploadRequestDTO request);
    }
}