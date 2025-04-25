using Domain.DTOs.Auth;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs.Auth;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthUserController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthUserController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var result = await _authService.LoginAsync(loginRequestDTO);
            return Ok(new { status = "success", token = result });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            var result = await _authService.RegisterAsync(registerRequestDTO);
            return Ok(new { status = "success", message = result });
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var result = await _authService.ConfirmEmailAsync(userId, token);
            if (result)
                return Ok("Email confirmed successfully!");

            return BadRequest("Email confirmation failed.");
        }
    }
}
