using Domain.DTOs.Order;

namespace Application.Mappings
{
    public class OrderProfile :
        BaseMappingProfileWithUpdate<Order, AddOrderDTO, UpdateOrderDTO, OrderDTO>
    {
    }
}