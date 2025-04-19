namespace Domain.DTOs.CartItem
{
    public class AddCartItemDTO
    {
        public int Quantity { get; set; }

        public int CartId { get; set; }

        public int ProductId { get; set; }
    }
}
