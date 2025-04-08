using Application.DTOs.Image;
using Application.Interfaces;
using Domain.Models;
using Infrastructure.Repositories.Interfaces;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IImageRepository _imageRepo;
        private readonly IUserRepository _userRepo;

        public UserService(IImageRepository imageRepo, IUserRepository userRepo)
        {
            _imageRepo = imageRepo;
            _userRepo = userRepo;
        }

        public async Task<ApplicationUser> UploadUserImageAsync(string userId, ImageUploadRequestDTO request)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("User not found");
            }

            var user = await _userRepo.GetUserByID(userId);
            ValidateFileUpload(request);

            var image = new Image
            {
                File = request.File,
                FileName = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                FileExtension = Path.GetExtension(request.File.FileName),
                FileSize = request.File.Length
            };

            await _imageRepo.Upload(image);
            user.ImageId = image.Id;
            await _userRepo.UpdateAsync(user);

            return user;
        }

        private void ValidateFileUpload(ImageUploadRequestDTO request)
        {
            if (request.File == null)
            {
                throw new Exception("File is required");
            }
            if (request.File.Length == 0)
            {
                throw new Exception("File is empty");
            }
            if (request.File.Length > 10 * 1024 * 1024)
            {
                throw new Exception("File is too large");
            }
            if (request.File.ContentType != "image/jpeg" && request.File.ContentType != "image/png")
            {
                throw new Exception("File is not an image");
            }
        }
    }
}