using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configuration
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasMany(u => u.SentMessages)
                   .WithOne(m => m.Sender)
                   .HasForeignKey(m => m.SenderID)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(u => u.ReceivedMessages)
                   .WithOne(m => m.Receiver)
                   .HasForeignKey(m => m.ReceiverID)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
