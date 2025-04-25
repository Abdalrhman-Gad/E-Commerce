using Application.Interfaces;
using Domain.DTOs.Payment;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        // GET: api/<PaymentController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDTO>>> Get([FromQuery] int? orderId, [FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1)
        {
            var payments = await _paymentService
                .GetAllAsync(
                filter: p => p.OrderId == orderId,
                pageSize: pageSize,
                pageNumber: pageNumber);

            return Ok(payments);
        }

        // GET api/<PaymentController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDTO>> Get(int id)
        {
            var payment = await _paymentService.GetByIdAsync(id);

            return Ok(payment);
        }

        // POST api/<PaymentController>
        [HttpPost]
        public async Task<ActionResult<PaymentDTO>> Post([FromBody] AddPaymentDTO addPayment)
        {
            var createdPayment = await _paymentService.AddAsync(addPayment);

            return CreatedAtAction(nameof(Get), new { id = createdPayment.PaymentId }, createdPayment);
        }
    }
}