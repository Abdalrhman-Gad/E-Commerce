using Domain.DTOs.Image;

namespace Application.Interfaces
{
    public interface IImageService
    {
        void ValidateImage(ImageUploadRequestDTO request);

        Task<int> UploadImageAsync(ImageUploadRequestDTO request);

        Task DeleteAsync(int imageId);
    }
}
