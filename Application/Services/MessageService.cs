using Application.Interfaces;
using Domain.DTOs.Message;
using Domain.Enums;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Infrastructure.Persistence;
using Domain.DTOs;

namespace Application.Services
{
    public class MessageService : IMessageService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MessageService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MessageDto> SendMessageAsync(CreateMessageDto dto)
        {
            var conversation = await _context.Conversations
                .FirstOrDefaultAsync(c =>
                    (c.FirstUserID == dto.SenderID && c.SecondUserID == dto.ReceiverID) ||
                    (c.FirstUserID == dto.ReceiverID && c.SecondUserID == dto.SenderID));

            if (conversation == null)
            {
                conversation = new Conversation
                {
                    FirstUserID = dto.SenderID,
                    SecondUserID = dto.ReceiverID,
                    CreatedDate = DateTime.UtcNow
                };
                _context.Conversations.Add(conversation);
                await _context.SaveChangesAsync();
            }

            var message = new Message
            {
                ConversationID = conversation.ConversationID,
                SenderID = dto.SenderID,
                ReceiverID = dto.ReceiverID,
                MessageText = dto.MessageText,
                MessageDate = DateTime.UtcNow,
                MessageStatus = MessageStatus.Sent
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            return _mapper.Map<MessageDto>(message);
        }


        public async Task<IEnumerable<MessageDto>> GetUnreadMessagesForAdminAsync()
        {
            // Retrieve unread messages for admins
            var messages = await _context.Messages
                .Where(m => m.MessageStatus == MessageStatus.Sent)
                .Include(m => m.Sender)
                .ToListAsync();

            // Map messages to MessageDto
            return _mapper.Map<IEnumerable<MessageDto>>(messages);
        }

        public async Task<IEnumerable<MessageDto>> GetConversationMessagesAsync(int conversationId)
        {
            // Get all messages for a specific conversation
            var messages = await _context.Messages
                .Where(m => m.ConversationID == conversationId)
                .OrderBy(m => m.MessageDate)
                .ToListAsync();

            // Map messages to MessageDto
            return _mapper.Map<IEnumerable<MessageDto>>(messages);
        }

        public async Task MarkAsReadAsync(int messageId)
        {
            // Find the message by its ID
            var message = await _context.Messages.FindAsync(messageId);
            if (message != null)
            {
                // Update message status to 'Read'
                message.MessageStatus = MessageStatus.Read;
                await _context.SaveChangesAsync();
            }
        }
    }
}
