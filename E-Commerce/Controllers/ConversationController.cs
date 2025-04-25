using Application.Interfaces;
using Application.Services;
using Domain.DTOs.Conversation;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversationController : ControllerBase
    {
        private readonly IConversationService _conversationService;

        public ConversationController(IConversationService conversationService)
        {
            _conversationService = conversationService;
        }

        // Create a new conversation (if it doesn't exist)
        [HttpPost("create")]
        public async Task<IActionResult> CreateConversation([FromBody] CreateConversationDto createConversationDto)
        {
            var conversation = await _conversationService.CreateConversationAsync(createConversationDto);
            return Ok(conversation);
        }

        // Get all conversations for a specific user
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserConversations(string userId)
        {
            var conversations = await _conversationService.GetConversationsByUserIdAsync(userId);
            return Ok(conversations);
        }

        //[Authorize(Roles = "admin")]
        [HttpGet("admin/unread-conversations")]
        public async Task<IActionResult> GetUnreadConversationsForAdmins()
        {
            var conversations = await _conversationService.GetAllConversationsWithUnreadMessagesForAdminsAsync();
            return Ok(conversations);
        }

        // Get a specific conversation by ID
        [HttpGet("{conversationId}")]
        public async Task<IActionResult> GetConversationById(int conversationId)
        {
            var conversation = await _conversationService.GetConversationByIdAsync(conversationId);
            return Ok(conversation);
        }

        // Optionally, you could add additional endpoints here to manage conversations, e.g., delete conversations, mark as read, etc.
    }
}
