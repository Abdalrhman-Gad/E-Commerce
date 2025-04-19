using Application.Interfaces;
using Domain.DTOs.Review;
using Domain.DTOs.ShippingAddress;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingAddressController : ControllerBase
    {
        private readonly IShippingAddressService _shippingAddressService;

        public ShippingAddressController(IShippingAddressService shippingAddressService)
        {
            _shippingAddressService = shippingAddressService;
        }

        // GET: api/<ShippingAddressController>
        [Authorize(Roles = "user")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShippingAddressDTO>>> Get([FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1)
        {
            var reviews = await _shippingAddressService.GetAllAsync(
                pageSize: pageSize, pageNumber: pageNumber
            );

            return Ok(reviews);
        }

        // GET api/<ShippingAddressController>/5
        [Authorize(Roles = "user")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewDTO>> Get(int id)
        {
            var shippingAddress = await _shippingAddressService.GetByIdAsync(id);

            return Ok(shippingAddress);
        }

        // POST api/<ShippingAddressController>
        [Authorize(Roles = "user")]
        [HttpPost]
        public async Task<ActionResult<ReviewDTO>> Post([FromBody] AddShippingAddressDTO addShippingAddress)
        {
            var createdShippingAddress = await _shippingAddressService
                .AddAsync(addShippingAddress);

            return CreatedAtAction(nameof(Get), new
            {
                id = createdShippingAddress.ShippingAddressId
            },
                createdShippingAddress
            );
        }

        // PUT api/<ShippingAddressController>/5
        [Authorize(Roles = "user")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ReviewDTO>> Put(int id, [FromBody] AddShippingAddressDTO updateShippingAddress)
        {
            var updatedShippingAddress = await _shippingAddressService
                .UpdateAsync(id, updateShippingAddress);

            return Ok(updatedShippingAddress);
        }

        // DELETE api/<ShippingAddressController>/5
        [Authorize(Roles = "user")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _shippingAddressService.DeleteAsync(id);

            return NoContent();
        }
    }
}