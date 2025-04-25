using Domain.DTOs.Image;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("uploadUserImage")]
        [Authorize]
        public async Task<IActionResult> UploadUserImage([FromForm] ImageUploadRequestDTO request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
               return Unauthorized("User ID not found.");
            }

            var result = await userService.UploadUserImageAsync(userId, request);

            if (!result)
            {
                return BadRequest("Image upload failed");
            }

            return Ok(new { status = "success", message = "Image uploaded successfully." });
        }
    }
}