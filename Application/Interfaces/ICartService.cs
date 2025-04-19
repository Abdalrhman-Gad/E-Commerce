using Application.Interfaces.IServices;
using Domain.DTOs.Cart;

namespace Application.Interfaces
{
    public interface ICartService : ICreatableService<AddCartDTO, CartDTO>, IDeletableService<string>
    {
        Task<CartDTO> GetByUserIdAsync(string userId);
    }
}