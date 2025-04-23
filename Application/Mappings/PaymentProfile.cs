using Application.Mappings.BaseProfiles;
using Domain.DTOs.Payment;
using Domain.Models;

namespace Application.Mappings
{
    public class PaymentProfile : BaseMappingProfile<Payment, AddPaymentDTO, PaymentDTO>
    {
    }
}