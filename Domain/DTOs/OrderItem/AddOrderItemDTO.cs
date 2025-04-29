namespace Domain.DTOs.OrderItem
{
    public class AddOrderItemDTO
    {
        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public int ProductId { get; set; }
    }
}