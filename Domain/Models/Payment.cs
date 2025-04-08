using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

        [Required,MaxLength(15)]
        public PaymentMethod PaymentMethod { get; set; }

        [Required,MaxLength(10)]
        public PaymentStatus PaymentStatus { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}