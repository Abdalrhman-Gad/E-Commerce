using Domain.Models;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IMessageRepository : IRepository<Message>
    {
        Task<IEnumerable<Message>> GetMessagesByConversationIdAsync(int conversationId);
        Task<IEnumerable<Message>> GetUnreadMessagesByUserAsync(string userId);
    }
}