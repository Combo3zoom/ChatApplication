using ChatApplication.Database.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApplication.Database.Data.Configurations;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.HasMany(u => u.JoinedChats)
            .WithMany(c => c.JoinedUsers)
            .UsingEntity<Dictionary<string, object>>(
                "UserChat",
                j => j.HasOne<Chat>().WithMany().HasForeignKey("ChatId"),
                j => j.HasOne<User>().WithMany().HasForeignKey("UserId")
            );

        builder.HasMany(u => u.CreatedChats)
            .WithOne(c => c.Owner)
            .HasForeignKey(c => c.OwnerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(u => u.Name)
            .IsRequired();
        
    }
}