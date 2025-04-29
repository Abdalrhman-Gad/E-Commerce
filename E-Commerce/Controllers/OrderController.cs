using Application.Interfaces;
using Domain.DTOs.Order;
using Domain.Exceptions.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/<OrderController>
        [Authorize(Roles = "admin,user")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> Get([FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var orders = await _orderService.GetAllAsync(
                   filter: o => o.UserId == userId,
                   pageSize: pageSize,
                   pageNumber: pageNumber);

            return Ok(orders);
        }

        // GET api/<OrderController>/5
        [Authorize(Roles = "admin,user")]
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> Get(int id)
        {
            try
            {
                var order = await _orderService.GetByIdAsync(id);

                return Ok(order);
            }
            catch (OrderNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST api/<OrderController>
        [Authorize(Roles = "user")]
        [HttpPost]
        public async Task<ActionResult<OrderDTO>> Post([FromBody] AddOrderDTO addOrder)
        {
            try 
            {
                var createdOrder = await _orderService.AddAsync(addOrder);

                return CreatedAtAction(nameof(Get), new { id = createdOrder.OrderId }, createdOrder);
            }
            catch (InvalidOrderException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        // PUT api/<OrderController>/5
        [Authorize(Roles = "admin,user")]
        [HttpPut("{id}")]
        public async Task<ActionResult<OrderDTO>> Put(int id, [FromBody] UpdateOrderDTO updateOrder)
        {
            try
            {
                var updatedOrder = await _orderService.UpdateAsync(id, updateOrder);

                return Ok(updatedOrder);
            }
            catch (OrderNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOrderException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE api/<OrderController>/5
        [Authorize(Roles = "admin,user")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _orderService.DeleteAsync(id);

                return NoContent();
            }
            catch (OrderNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}