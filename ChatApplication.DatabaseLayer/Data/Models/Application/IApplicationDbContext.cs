using Microsoft.EntityFrameworkCore;

namespace ChatApplication.Database.Data.Models.Application;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<Chat> Chats { get; }
    DbSet<Message> Messages { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}