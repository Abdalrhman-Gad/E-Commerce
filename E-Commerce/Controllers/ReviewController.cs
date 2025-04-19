using Application.Interfaces;
using Domain.DTOs.Review;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        // GET: api/<ReviewsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewDTO>>> Get([FromQuery] int productId, [FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1)
        {
            var reviews = await _reviewService.GetAllAsync(
                filter: r => r.ProductId == productId,
                pageSize: pageSize, pageNumber: pageNumber
            );

            return Ok(reviews);
        }

        // GET api/<ReviewsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewDTO>> Get(int id)
        {
            var review = await _reviewService.GetByIdAsync(id);

            return Ok(review);
        }

        // POST api/<ReviewsController>
        [Authorize(Roles = "admin,user")]
        [HttpPost]
        public async Task<ActionResult<ReviewDTO>> Post([FromBody] AddReviewDTO addReview)
        {
            var createdReview = await _reviewService.AddAsync(addReview);

            return CreatedAtAction(nameof(Get), new { id = addReview.ProductId }, createdReview);
        }

        // PUT api/<ReviewsController>/5
        [Authorize(Roles = "admin,user")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ReviewDTO>> Put(int id, [FromBody] UpdateReviewDTO updateReview)
        {
            var updatedReview = await _reviewService.UpdateAsync(id, updateReview);

            return Ok(updatedReview);
        }

        // DELETE api/<ReviewsController>/5
        [Authorize(Roles = "admin,user")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _reviewService.DeleteAsync(userId);

            return NoContent();
        }
    }
}