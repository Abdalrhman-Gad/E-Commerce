namespace Domain.DTOs.Product
{
    public class GetProductDTO
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public decimal? Discount { get; set; }

        public int? ImageId { get; set; }

        public string ImageUrl { get; set; } // Optional, if you want to show image

        public int CategoryId { get; set; }

        public string CategoryName { get; set; } // Optional, to show category name
    }
}