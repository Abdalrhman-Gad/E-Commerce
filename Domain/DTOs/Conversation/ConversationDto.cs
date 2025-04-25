using Domain.DTOs.Message;

namespace Domain.DTOs.Conversation
{
    public class ConversationDto
    {
        public int ConversationID { get; set; }
        public string FirstUserID { get; set; }
        public string SecondUserID { get; set; }
        public DateTime CreatedDate { get; set; }

        public List<MessageDto> Messages { get; set; }
    }
}
