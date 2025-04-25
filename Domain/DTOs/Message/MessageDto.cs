using Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Domain.DTOs.Message
{
    public class MessageDto
    {
        public int MessageID { get; set; }
        public int ConversationID { get; set; }
        public string SenderID { get; set; }
        public string ReceiverID { get; set; }
        public string MessageText { get; set; }
        public DateTime MessageDate { get; set; }
        public string MessageStatus { get; set; }
    }
}
