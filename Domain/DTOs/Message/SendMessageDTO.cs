namespace Domain.DTOs.Message
{
    public class SendMessageDTO
    {
        public string ReceiverID { get; set; }  // ID of the user receiving the message
        public string MessageText { get; set; }  // Text content of the message
    }
}
