using Domain.Exceptions.Payment;
using Domain.Models;
using Application.Interfaces;
using AutoMapper;
using Infrastructure.Repositories.Interfaces;
using Domain.DTOs.Payment;
using System.Linq.Expressions;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IMapper _mapper;

    public PaymentService(IPaymentRepository paymentRepository, IMapper mapper)
    {
        _paymentRepository = paymentRepository;
        _mapper = mapper;
    }

    public async Task<PaymentDTO> AddAsync(AddPaymentDTO addPayment)
    {
        if (addPayment == null)
            throw new InvalidPaymentException("Payment data is invalid.");

        var payment = _mapper.Map<Payment>(addPayment);

        await _paymentRepository.AddAsync(payment);

        return _mapper.Map<PaymentDTO>(payment);
    }

    public async Task<IEnumerable<PaymentDTO>> GetAllAsync(Expression<Func<Payment, bool>>? filter = null, int pageSize = 0, int pageNumber = 1)
    {
        var payments = await _paymentRepository
            .GetAllAsync(filter: filter, pageSize: pageSize, pageNumber: pageNumber);

        return _mapper.Map<IEnumerable<PaymentDTO>>(payments);
    }

    public async Task<PaymentDTO> GetByIdAsync(int id)
    {
        var payment = await _paymentRepository
            .GetAsync(p => p.PaymentId == id)
            ?? throw new PaymentNotFoundException("Payment not found.");

        return _mapper.Map<PaymentDTO>(payment);
    }
}