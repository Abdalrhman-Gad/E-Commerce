using Domain.DTOs.OrderItem;
using Domain.Enums;

namespace Domain.DTOs.Order
{
    public class AddOrderDTO
    {
        public decimal TotalAmount { get; set; }

        public OrderStatus Status { get; set; }

        public string UserId { get; set; }

        public int OrderShippingAddressId { get; set; }

        public ICollection<AddOrderItemDTO> OrderItems { get; set; } = [];
    }
}