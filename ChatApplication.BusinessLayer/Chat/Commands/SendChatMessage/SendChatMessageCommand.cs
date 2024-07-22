using Ardalis.GuardClauses;
using ChatApplication.Database.Data.Models.Application;
using ChatApplication.Services.Message.Queries.GetByIdMessage;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.Services.Chat.Commands.SendChatMessage;

public record SendChatMessageCommand(
    uint ChatId, 
    uint UserId, 
    string Message): IRequest<SendChatMessageResponse>;

public record SendChatMessageBody(string Message): IRequest<SendChatMessageResponse>;

public class SendChatMessageCommandHandler(
    IApplicationDbContext context,
    IMapper mapper)
    : IRequestHandler<SendChatMessageCommand, SendChatMessageResponse>
{
    public async Task<SendChatMessageResponse> Handle(SendChatMessageCommand request, CancellationToken cancellationToken)
    {
        var chatId = await context.Chats
            .Select(c => c.Id)
            .FirstOrDefaultAsync(id => id == request.ChatId, cancellationToken);

        Guard.Against.NotFound(request.ChatId, chatId);

        var userId = await context.Chats
            .Select(user => user.Id)
            .FirstOrDefaultAsync(id => id == request.UserId, cancellationToken);

        Guard.Against.NotFound(request.UserId, userId);

        var message = new Database.Data.Models.Message(default,
            userId,
            null,
            chatId,
            null,
            request.Message);

        context.Messages.Add(message);
        
        await context.SaveChangesAsync(cancellationToken);

        var isChatMember = await context.Chats
            .Where(c => c.JoinedUsers.Any(u => u.Id == userId) && c.Id == chatId)
                                        .Select(c => c.Id)
                                        .AnyAsync();
        if (!isChatMember)
            throw new Exception("Chat has no users");
        
        return await context.Users
            .Where(u => u.Id == userId)
            .Select(user=> mapper.Map<SendChatMessageResponse>(user))
            .FirstAsync(cancellationToken: cancellationToken);
    }
}