using Application.Mappings.BaseProfiles;
using Domain.DTOs.OrderItem;
using Domain.Models;

namespace Application.Mappings
{
    public class OrderItemProfile : 
        BaseMappingProfile<OrderItem,AddOrderItemDTO,OrderItemDTO>
    {
    }
}
