using Application.DTOs.Image;
using Application.Interfaces.IServices;
using Domain.DTOs.Product;
using Domain.Models;

namespace Application.Interfaces
{
    public interface IProductService :
        IReadableService<Product,GetProductDTO>, 
        IUpdatableService<GetProductDTO,AddProductDTO>,
        ICreatableService<AddProductDTO, GetProductDTO>,
        IDeletableService
    {
        Task<bool> UploadProductImageAsync(int productId, ImageUploadRequestDTO request);
    }
}