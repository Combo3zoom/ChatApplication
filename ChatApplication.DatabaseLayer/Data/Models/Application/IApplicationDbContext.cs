using Microsoft.EntityFrameworkCore;

namespace ChatApplication.database.Data.Models.Application;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<Chat> Chats { get; }
    DbSet<Message> Messages { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}