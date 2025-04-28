using Domain.DTOs.Product;

namespace Domain.DTOs.OrderItem
{
    public class OrderItemDTO
    {
        public int OrderItemId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public int OrderId { get; set; }

        public GetProductDTO Product { get; set; }
    }
}