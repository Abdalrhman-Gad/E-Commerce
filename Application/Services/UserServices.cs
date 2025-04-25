using Domain.DTOs.Image;
using Application.Interfaces;
using Domain.Exceptions.User;
using Infrastructure.Repositories.Interfaces;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IImageService _imageService;

        public UserService(IImageService imageService, IUserRepository userRepository)
        {
            _imageService = imageService;
            _userRepository = userRepository;
        }

        public async Task<bool> UploadUserImageAsync(string userId, ImageUploadRequestDTO request)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new UserNotFoundException(userId);

            var user = await _userRepository.GetUserByID(userId)
                ?? throw new UserNotFoundException(userId);

            // Delete the previous image if any
            if (user.ImageId.HasValue)
            {
                await _imageService.DeleteAsync((int)user.ImageId);
            }

            var imageId = await _imageService.UploadImageAsync(request);

            user.ImageId = imageId;
            await _userRepository.UpdateAsync(user);

            return true;
        }
    }
}