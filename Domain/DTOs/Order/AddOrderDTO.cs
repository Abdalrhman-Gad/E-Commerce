using Domain.DTOs.OrderItem;
using System.Text.Json.Serialization;

namespace Domain.DTOs.Order
{
    public class AddOrderDTO
    {
        public decimal TotalAmount { get; set; }

        public int OrderShippingAddressId { get; set; }

        public ICollection<AddOrderItemDTO> OrderItems { get; set; } = [];
    }
}