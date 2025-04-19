using Application.Interfaces;
using Application.Services;
using Domain.DTOs.Cart;
using Domain.DTOs.Review;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        // GET api/<ReviewsController>/5
        [Authorize(Roles = "user")]
        [HttpGet]
        public async Task<ActionResult<ReviewDTO>> Get()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var review = await _cartService.GetByUserIdAsync(userId);

            return Ok(review);
        }

        // POST api/<ReviewsController>
        [Authorize(Roles = "user")]
        [HttpPost]
        public async Task<ActionResult<CartDTO>> Post()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var addCartDto = new AddCartDTO { UserId = userId };

            var createdCart = await _cartService.AddAsync(addCartDto);

            return CreatedAtAction(nameof(Get), new { id = createdCart.CartId }, createdCart);
        }


        // DELETE api/<ReviewsController>/5
        [Authorize(Roles = "user")]
        [HttpDelete]
        public async Task<ActionResult> Delete()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _cartService.DeleteAsync(userId);

            return NoContent();
        }
    }
}
