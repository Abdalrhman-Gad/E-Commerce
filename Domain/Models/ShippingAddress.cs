using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class ShippingAddress
    {
        [Key]
        public int ShippingAddressId { get; set; }

        [Required, MaxLength(200)]
        public string AddressLine1 { get; set; }

        [MaxLength(200)]
        public string AddressLine2 { get; set; }

        [Required, MaxLength(100)]
        public string City { get; set; }

        [Required, MaxLength(20)]
        public string PostalCode { get; set; }

        [Required, MaxLength(100)]
        public string Country { get; set; }

        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public ICollection<Order> Orders { get; set; } = [];
    }
}
