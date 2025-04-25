using Domain.Enums;

namespace Domain.DTOs.Payment
{
    public class AddPaymentDTO
    {
        public PaymentMethod PaymentMethod { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        public int OrderId { get; set; }
    }
}