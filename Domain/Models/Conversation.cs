using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Conversation
    {
        [Key]
        public int ConversationID { get; set; }

        public string FirstUserID { get; set; }

        public string SecondUserID { get; set; }

        public DateTime CreatedDate { get; set; }

        public ApplicationUser FirstUser { get; set; }

        public ApplicationUser SecondUser { get; set; }

        public ICollection<Message> Messages { get; set; } = [];
    }
}