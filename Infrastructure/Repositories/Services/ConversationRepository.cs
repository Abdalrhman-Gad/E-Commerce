using Domain.Models;
using Infrastructure.Persistence;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Repositories.Services
{
    public class ConversationRepository : Repository<Conversation>, IConversationRepository
    {
        private readonly ApplicationDbContext _db;

        public ConversationRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        // Get the conversation between a user and an admin
        public async Task<Conversation> GetConversationByUsersAsync(string userId, string adminId)
        {
            return await _db.Conversations
                .FirstOrDefaultAsync(c =>
                    (c.FirstUserID == userId && c.SecondUserID == adminId) ||
                    (c.FirstUserID == adminId && c.SecondUserID == userId));
        }

        // Get unread conversations for an admin
        public async Task<IEnumerable<Conversation>> GetUnreadConversationsForAdminAsync(string adminId)
        {
            return await _db.Conversations
                .Where(c => c.FirstUserID == adminId || c.SecondUserID == adminId)
                .ToListAsync();
        }
    }
}
