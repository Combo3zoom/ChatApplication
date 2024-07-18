using Ardalis.GuardClauses;
using ChatApplication.Database.Data.Models.Application;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ChatApplication.Services.Chat.Commands.SendChatMessage;

namespace ChatApplication.Services.Chat.Commands.UpdateSendMessagesChat;

public record SendChatMessageCommand(
    uint ChatId, 
    uint UserId, 
    string Message): IRequest<SendChatMessageResponse>;

public record SendChatMessageBody(string Message): IRequest<SendChatMessageResponse>;

public class SendChatMessageCommandHandler(
    IApplicationDbContext context)
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

        var username = await context.Users
            .Where(u => u.Id == userId)
            .Select(user => user.Name)
            .FirstAsync();

        return new SendChatMessageResponse(username);
    }
}