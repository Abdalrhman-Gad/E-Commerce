using Domain.Enums;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder
                .HasOne(m => m.Sender)
                .WithMany()
                .HasForeignKey(m => m.SenderID)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(m => m.Conversation)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.ConversationID)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Property(m => m.MessageStatus)
                .HasConversion(
                    v => v.ToString(),
                    v => (MessageStatus)Enum.Parse(typeof(MessageStatus), v));

            builder.Property(m => m.MessageDate)
                   .HasDefaultValueSql("GETUTCDATE()")
                   .ValueGeneratedOnAdd();
        }
    }
}
