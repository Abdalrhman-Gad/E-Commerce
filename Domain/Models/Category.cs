using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public int? ImageId { get; set; }

        [ForeignKey("ImageId")]
        public Image Image { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
