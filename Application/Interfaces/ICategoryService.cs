using Application.DTOs.Image;
using Application.Interfaces.IServices;
using Domain.DTOs.Category;
using Domain.DTOs.Product;
using Domain.Models;

namespace Application.Interfaces
{
    public interface ICategoryService :
        IReadableService<Category, GetCategoryDTO>,
        IUpdatableService<GetCategoryDTO, AddCategoryDTO>,
        ICreatableService<AddCategoryDTO, GetCategoryDTO>,
        IDeletableService<int>
    {
        Task<IEnumerable<GetCategoryDTO>> SearchByNameAsync(string name);
        Task<bool> UploadCategoryImageAsync(int categoryId, ImageUploadRequestDTO request);
    }
}
