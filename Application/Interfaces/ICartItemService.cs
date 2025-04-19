using Application.Interfaces.IServices;
using Domain.DTOs.CartItem;
using Domain.Models;

namespace Application.Interfaces
{
    public interface ICartItemService : IReadableService<CartItem, CartItemDTO>
    {
    }
}
