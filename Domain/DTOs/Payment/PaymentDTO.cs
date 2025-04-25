using Domain.Enums;

namespace Domain.DTOs.Payment
{
    public class PaymentDTO
    {
        public int PaymentId { get; set; }

        public DateTime PaymentDate { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        public int OrderId { get; set; }
    }
}