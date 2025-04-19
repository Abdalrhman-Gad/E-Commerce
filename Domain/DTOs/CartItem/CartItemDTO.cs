using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.CartItem
{
    public class CartItemDTO
    {
        public int CartItemId { get; set; }

        public int Quantity { get; set; }

        public int CartId { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        public decimal ProductPrice { get; set; }

        public int ProductStock { get; set; }

        public decimal? ProductDiscount { get; set; }

        public int? ImageUrl { get; set; }
    }
}
