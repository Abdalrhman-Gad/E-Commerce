using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class ConversationConfiguration : IEntityTypeConfiguration<Conversation>
    {
        public void Configure(EntityTypeBuilder<Conversation> builder)
        {
            builder
                .HasOne(c => c.SecondUser)
                .WithMany()  // One user can be part of many conversations
                .HasForeignKey(c => c.SecondUserID)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(c => c.CreatedDate)
                   .HasDefaultValueSql("GETUTCDATE()")
                   .ValueGeneratedOnAdd();
        }
    }
}
