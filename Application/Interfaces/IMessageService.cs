using Domain.DTOs;
using Domain.DTOs.Message;

namespace Application.Interfaces
{
    public interface IMessageService
    {
        Task<MessageDto> SendMessageAsync(CreateMessageDto dto);
        Task<IEnumerable<MessageDto>> GetUnreadMessagesForAdminAsync();
        Task<IEnumerable<MessageDto>> GetConversationMessagesAsync(int conversationId);
        Task MarkAsReadAsync(int messageId);
    }
}
