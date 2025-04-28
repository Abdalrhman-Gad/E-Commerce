using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace E_Commerce.Hubs
{
    public class ChatHub : Hub
    {
        // Send a message to a specific user
        public async Task SendMessage(string senderId, string receiverId, string messageText)
        {
            await Clients.User(receiverId).SendAsync("ReceiveMessage", senderId, messageText);
            await Clients.User(senderId).SendAsync("ReceiveMessage", senderId, messageText); // To notify the sender if needed
        }

        // Notify the receiver when the sender is typing
        public async Task SendTypingNotification(string senderId, string receiverId)
        {
            await Clients.User(receiverId).SendAsync("ReceiveTypingNotification", senderId);
        }

        // Join a specific conversation
        public async Task JoinConversation(int conversationId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, conversationId.ToString());
        }

        // Leave a specific conversation
        public async Task LeaveConversation(int conversationId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, conversationId.ToString());
        }
    }
}