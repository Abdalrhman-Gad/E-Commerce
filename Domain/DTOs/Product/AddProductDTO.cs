using Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Domain.DTOs.Product
{
    public class AddProductDTO
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public decimal? Discount { get; set; }

        public int? ImageId { get; set; }

        public int CategoryId { get; set; }
    }
}
