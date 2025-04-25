using Application.Interfaces.IServices;
using Domain.DTOs.Payment;
using Domain.Models;

namespace Application.Interfaces
{
    public interface IPaymentService :
        IReadableService<Payment, PaymentDTO>,
        ICreatableService<AddPaymentDTO, PaymentDTO>
    {
    }
}