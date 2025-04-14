using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Category
{
    public class AddCategoryDTO
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public int? ImageId { get; set; }
    }
}

