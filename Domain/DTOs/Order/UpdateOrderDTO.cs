using Domain.DTOs.OrderItem;
using Domain.Enums;

namespace Domain.DTOs.Order
{
    public class UpdateOrderDTO
    {
        public decimal TotalAmount { get; set; }

        public OrderStatus Status { get; set; }

        public int OrderShippingAddressId { get; set; }

        public List<OrderItemDTO> OrderItems { get; set; } = [];
    }
}