using Ardalis.GuardClauses;
using ChatApplication.Database.Data.Models.Application;
using ChatApplication.database.Services.Service;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.Services.Chat.Commands.AddChatUser;

public record AddChatUserCommand(uint ChatId, uint UserId)
    : IRequest;

public class AddChatUserCommandHandler(
    IApplicationDbContext context,
    IChatService chatService)
    : IRequestHandler<AddChatUserCommand>
{
    public async Task Handle(AddChatUserCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Users
            .Where(user => user.Id == request.UserId)
            .FirstOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(request.UserId, user);

        var chat = await context.Chats
            .Where((c) => c.Id == request.ChatId)
            .Include(c => c.JoinedUsers)
            .FirstOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(request.ChatId, chat);

        chat.JoinedUsers.Add(user);

        await context.SaveChangesAsync(cancellationToken);

        await chatService.OnNewChatUserAdded(user.Id, chat.Id);
    }
}