using Ardalis.GuardClauses;
using ChatApplication.Database.Data.Models;
using ChatApplication.Database.Data.Models.Application;
using ChatApplication.Database.Services;
using ChatApplication.database.Services.Service;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.Services.Chat.Commands.UpdateSendMessagesChat;

public record SendChatMessageCommand(
    uint ChatId, 
    uint UserId, 
    string Message) : IRequest;

public record SendChatMessageBody(string Message);
public class SendChatMessageCommandCommandHandler(
    IApplicationDbContext context,
    IChatService chatService)
    : IRequestHandler<SendChatMessageCommand>
{
    public async Task Handle(SendChatMessageCommand request, CancellationToken cancellationToken)
    {
        context.Chats.AnyAsync(chat => chat.Id == request.ChatId);
        
        var message = new Database.Data.Models.Message(default,
            request.UserId,
            null,
            request.ChatId,
            null,
            request.Message);

        context.Messages.Add(message);
        
        await context.SaveChangesAsync(cancellationToken);

        await chatService.SendMessage(request.UserId, request.ChatId, request.Message);
    }
}