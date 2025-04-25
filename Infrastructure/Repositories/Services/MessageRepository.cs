using Domain.Enums;
using Domain.Models;
using Infrastructure.Persistence;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Repositories.Services
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        private readonly ApplicationDbContext _db;

        public MessageRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        // Get all messages in a conversation
        public async Task<IEnumerable<Message>> GetMessagesByConversationIdAsync(int conversationId)
        {
            return await _db.Messages
                .Where(m => m.ConversationID == conversationId)
                .OrderBy(m => m.MessageDate)
                .ToListAsync();
        }

        // Get unread messages for a user
        public async Task<IEnumerable<Message>> GetUnreadMessagesByUserAsync(string userId)
        {
            return await _db.Messages
                .Where(m => (m.SenderID == userId || m.ReceiverID == userId) && m.MessageStatus == MessageStatus.Sent)
                .ToListAsync();
        }
    }
}
