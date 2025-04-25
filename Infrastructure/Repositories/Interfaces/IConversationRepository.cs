using Domain.Models;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IConversationRepository : IRepository<Conversation>
    {
        Task<Conversation> GetConversationByUsersAsync(string userId, string adminId);
        Task<IEnumerable<Conversation>> GetUnreadConversationsForAdminAsync(string adminId);
    }
}
