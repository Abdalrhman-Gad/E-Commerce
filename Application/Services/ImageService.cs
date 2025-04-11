using Application.DTOs.Image;
using Application.Interfaces;
using Domain.Exceptions.Image;
using Domain.Models;
using Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;
        private readonly ILogger<ImageService> _logger;

        public ImageService(IImageRepository imageRepository, ILogger<ImageService> logger)
        {
            _imageRepository = imageRepository;
            _logger = logger;
        }

        public async Task DeleteAsync(int imageId)
        {
            try
            {
                await _imageRepository.DeleteAsync(imageId);
                _logger.LogInformation($"Image with ID {imageId} deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting image with ID {imageId}.");
                throw new ImageServiceException("An error occurred while deleting the image.", ex);
            }
        }

        public async Task<int> UploadImageAsync(ImageUploadRequestDTO request)
        {
            try
            {
                ValidateImage(request);

                var newImage = new Image
                {
                    File = request.File,
                    FileName = $"{Guid.NewGuid()}_{DateTime.UtcNow:yyyyMMddHHmmssfff}",
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSize = request.File.Length
                };

                await _imageRepository.Upload(newImage);

                _logger.LogInformation($"Image uploaded successfully with ID {newImage.Id}.");
                return newImage.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading image.");
                throw new ImageServiceException("An error occurred while uploading the image.", ex);
            }
        }

        public void ValidateImage(ImageUploadRequestDTO request)
        {
            if (request.File == null)
                throw new ImageServiceException("No file uploaded.");

            if (request.File.Length == 0)
                throw new ImageServiceException("Uploaded file is empty.");

            if (request.File.Length > 10 * 1024 * 1024)
                throw new ImageServiceException("File size exceeds 10MB limit.");

            var allowedTypes = new[] { "image/jpeg", "image/png" };
            if (!allowedTypes.Contains(request.File.ContentType))
                throw new ImageServiceException("Unsupported file type. Only JPEG and PNG are allowed.");
        }
    }
}