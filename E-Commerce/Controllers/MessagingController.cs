using E_Commerce.Hubs;
using Domain.DTOs;
using Application.Interfaces;
using Domain.DTOs.Message;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;


namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagingController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IHubContext<ChatHub> _hubContext;

        public MessagingController(IMessageService messageService, IHubContext<ChatHub> hubContext)
        {
            _messageService = messageService;
            _hubContext = hubContext;
        }

        // Endpoint to send a message
        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] CreateMessageDto createMessageDto)
        {
            var message = await _messageService.SendMessageAsync(createMessageDto);
            await _hubContext.Clients.User(createMessageDto.ReceiverID)
                             .SendAsync("ReceiveMessage", createMessageDto.SenderID, createMessageDto.MessageText);

            return Ok(message);
        }


        // Endpoint to get unread messages for admin
        [HttpGet("unread")]
        public async Task<IActionResult> GetUnreadMessagesForAdmin()
        {
            var messages = await _messageService.GetUnreadMessagesForAdminAsync();
            return Ok(messages);
        }

        // Endpoint to mark a message as read
        [HttpPost("mark-as-read/{messageId}")]
        public async Task<IActionResult> MarkMessageAsRead(int messageId)
        {
            await _messageService.MarkAsReadAsync(messageId);
            return Ok();
        }

        // Endpoint to send typing notification
        [HttpPost("typing")]
        public async Task<IActionResult> SendTypingNotification([FromBody] TypingNotificationDto typingNotificationDto)
        {
            await _hubContext.Clients.User(typingNotificationDto.ReceiverID).SendAsync("ReceiveTypingNotification", typingNotificationDto.SenderID);
            return Ok();
        }
    }
}
