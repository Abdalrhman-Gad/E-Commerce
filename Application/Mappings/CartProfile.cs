using Application.Mappings.BaseProfiles;
using Domain.DTOs.Cart;
using Domain.Models;

namespace Application.Mappings
{
    public class CartProfile : 
        BaseMappingProfile<Cart,AddCartDTO,CartDTO>
    {
    }
}
