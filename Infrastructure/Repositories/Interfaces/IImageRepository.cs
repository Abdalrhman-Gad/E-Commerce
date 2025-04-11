using Domain.Models;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);

        Task DeleteAsync(int imageId);
    }
}