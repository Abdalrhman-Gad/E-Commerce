using Domain.DTOs.OrderItem;
using Domain.DTOs.Payment;
using Domain.Enums;

namespace Domain.DTOs.Order
{
    public class OrderDTO
    {
        public int OrderId { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public OrderStatus Status { get; set; }

        public string UserId { get; set; }

        public int OrderShippingAddressId { get; set; }

        public List<OrderItemDTO> OrderItems { get; set; } = [];

        public PaymentDTO Payment { get; set; }
    }
}