using Application.DTOs.Image;
using Domain.DTOs.Product;
using Domain.Models;

namespace Application.Interfaces
{
    public interface IProductService : IService<Product, AddProductDTO, GetProductDTO> 
        , IUpdatableService<GetProductDTO,AddProductDTO>
    {
        Task<bool> UploadProductImageAsync(int productId, ImageUploadRequestDTO request);
    }
}