using Ardalis.GuardClauses;
using ChatApplication.database.Data.Models;
using ChatApplication.database.Data.Models.Application;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace ChatApplication.Services.Chat.Commands.UpdateJoinedUsersChat;

public record UpdateJoinUserChatCommand(string ConnectionId, uint ChatId, database.Data.Models.User JoinUser)
    : IRequest;

public class UpdateJoinedUsersChatCommandHandler(
    IApplicationDbContext context)
    : IRequestHandler<UpdateJoinUserChatCommand>
{
    public async Task Handle(UpdateJoinUserChatCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.Chats
            .FindAsync(new object?[] { request.ChatId }, cancellationToken);
        
        Guard.Against.NotFound(request.ChatId, entity);

        entity.JoinedUsers.Add(request.JoinUser);

        await context.SaveChangesAsync(cancellationToken);
        
        
    }
}