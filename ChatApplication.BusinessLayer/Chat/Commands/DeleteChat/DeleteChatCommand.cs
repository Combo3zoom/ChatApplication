using Ardalis.GuardClauses;
using ChatApplication.Database.Data.Models.Application;
using ChatApplication.database.Services.Service;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.Services.Chat.Commands.DeleteChat;

public record DeleteChatCommand(uint UserId, uint ChatId) : IRequest;

public class DeleteChatCommandHandler(
    IApplicationDbContext context,
    IChatService chatService) 
    : IRequestHandler<DeleteChatCommand>
{
    public async Task Handle(DeleteChatCommand request, CancellationToken cancellationToken)
    {
        var chat = await context.Chats.FirstOrDefaultAsync(chat => chat.Id == request.ChatId, cancellationToken);
        
        Guard.Against.NotFound(request.ChatId, chat);
        
        if (chat.OwnerId != request.UserId) 
            throw new UnauthorizedAccessException("Only the owner of the chat can delete it.");
        
        context.Chats.Remove(chat);

        await context.SaveChangesAsync(cancellationToken);
    }
}