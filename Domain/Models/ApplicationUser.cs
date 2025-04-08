using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required, MaxLength(256)]
        public string Name { get; set; }

        public int? ImageId { get; set; }

        [ForeignKey("ImageId")]
        public Image Image { get; set; }

        public Cart Cart { get; set; }

        public ICollection<ShippingAddress> ShippingAddresses { get; set; } = [];

        public ICollection<Review> Reviews { get; set; } = [];

        public ICollection<Order> Orders { get; set; } = [];
    }
}