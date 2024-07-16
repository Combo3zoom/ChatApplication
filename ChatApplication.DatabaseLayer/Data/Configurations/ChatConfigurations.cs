using ChatApplication.database.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApplication.database.Data.Configurations;

public class ChatConfigurations: IEntityTypeConfiguration<Chat>
{
    public void Configure(EntityTypeBuilder<Chat> builder)
    {
        builder.HasKey(c => c.Id);
        
        builder.HasOne(c => c.Owner)
            .WithMany(u => u.CreatedChats)
            .HasForeignKey(c => c.OwnerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.Messages)
            .WithOne(m => m.Chat)
            .HasForeignKey(m => m.ChatId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.JoinedUsers)
            .WithMany(u => u.JoinedChats)
            .UsingEntity<Dictionary<string, object>>(
                "UserChat",
                j => j.HasOne<User>().WithMany().HasForeignKey("UserId"),
                j => j.HasOne<Chat>().WithMany().HasForeignKey("ChatId")
            );
        
        builder.Property(c => c.Name)
            .IsRequired();
    }
}