using Application.DTOs.Image;
using Domain.DTOs.Product;
using Domain.Models;

namespace Application.Interfaces
{
    public interface IProductService : IService<Product, AddProductDTO, GetProductDTO>
    {
        Task<IEnumerable<GetProductDTO>> GetByCategoryNameAsync(string categoryName, string? includes = null, int pageSize = 0, int pageNumber = 1);

        Task<bool> UploadProductImageAsync(int productId, ImageUploadRequestDTO request);
    }
}