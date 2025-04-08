using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        // Foreign Key to AspNetUsers
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public ICollection<CartItem> CartItems { get; set; } = [];
    }
}
