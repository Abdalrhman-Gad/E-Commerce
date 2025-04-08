using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Message
    {
        [Key]
        public int MessageID { get; set; }

        public int ConversationID { get; set; }  
        
        public string SenderID { get; set; } 
        
        public string ReceiverID { get; set; }

        [MaxLength(500)]
        public string MessageText { get; set; }

        public DateTime MessageDate { get; set; }

        [MaxLength(10)]
        public MessageStatus MessageStatus { get; set; } 

        public Conversation Conversation { get; set; }
        
        public ApplicationUser Sender { get; set; }
        
        public ApplicationUser Receiver { get; set; }
    }
}
