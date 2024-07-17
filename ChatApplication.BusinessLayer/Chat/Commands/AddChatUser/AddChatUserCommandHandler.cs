using Ardalis.GuardClauses;
using ChatApplication.Database.Data.Models.Application;
using ChatApplication.database.Services.Service;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.Services.Chat.Commands.AddChatUser;

public class AddChatUserCommandHandler(
    IApplicationDbContext context,
    IChatService chatService)
    : IRequestHandler<AddChatUserCommand>
{
    public async Task Handle(AddChatUserCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Users.Where(user => user.Id == request.UserId)
            .FirstOrDefaultAsync(cancellationToken);

        if (user == null)
            throw new Exception("User not found");
        
        var chat = await context.Chats
            .Include(c => c.JoinedUsers)
            .FirstOrDefaultAsync(((c)=> c.Id == request.ChatId), cancellationToken);
        
        Guard.Against.NotFound(request.ChatId, chat);

        chat.JoinedUsers.Add(user);

        await context.SaveChangesAsync(cancellationToken);

        await chatService.OnNewChatUserAdded(user.Id, chat.Id);
    }
}