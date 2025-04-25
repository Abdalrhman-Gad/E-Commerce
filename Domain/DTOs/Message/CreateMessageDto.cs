namespace Domain.DTOs
{
    public class CreateMessageDto
    {
        public int ConversationID { get; set; }
        public string SenderID { get; set; }
        public string ReceiverID { get; set; }
        public string MessageText { get; set; }
    }
}