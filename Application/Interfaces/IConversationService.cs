using Domain.DTOs.Conversation;

namespace Application.Interfaces
{
    public interface IConversationService
    {
        Task<ConversationDto> CreateConversationAsync(CreateConversationDto dto);
        Task<ConversationDto> GetConversationByIdAsync(int conversationId);
        Task<IEnumerable<ConversationDto>> GetAllConversationsAsync();
        Task<IEnumerable<ConversationDto>> GetConversationsByUserIdAsync(string userId);
        Task<List<ConversationDto>> GetAllConversationsWithUnreadMessagesForAdminsAsync();
    }
}
