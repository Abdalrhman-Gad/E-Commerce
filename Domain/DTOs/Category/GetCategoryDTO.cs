using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOs.Product;
namespace Domain.DTOs.Category
{
    public class GetCategoryDTO
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ImageId { get; set; }
        public string ImageUrl { get; set; }
        //public List<GetProductDTO> Products { get; set; }// Include list of Product DTOs if needed
    }
}


