using Application.DTOs.Image;
using Domain.DTOs.Category;
using Domain.Models;

namespace Application.Interfaces
{
    public interface ICategoryService : IService<Category, AddCategoryDTO, GetCategoryDTO>
    {
        Task<IEnumerable<GetCategoryDTO>> SearchByNameAsync(string name);
        Task<bool> UploadCategoryImageAsync(int categoryId, ImageUploadRequestDTO request);
    }
}
