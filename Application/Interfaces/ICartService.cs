using Domain.DTOs.Cart;
using Domain.Models;

namespace Application.Interfaces
{
    public interface ICartService : IService<Cart,AddCartDTO,CartDTO>
    {
        Task<CartDTO> GetByUserIdAsync(string userId);
    }
}