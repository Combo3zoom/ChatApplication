using Ardalis.GuardClauses;
using ChatApplication.database.Data.Models.Application;
using MediatR;

namespace ChatApplication.Services.Chat.Commands.DeleteChat;

public record DeleteChatCommand(uint Id, uint UserId) : IRequest;

public class DeleteChatCommandHandler(IApplicationDbContext context) : IRequestHandler<DeleteChatCommand>
{
    public async Task Handle(DeleteChatCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.Chats.FindAsync(new object[] { request.Id }, cancellationToken);
        
        Guard.Against.NotFound(request.Id, entity);
        
        if (entity.OwnerId != request.UserId) 
            throw new UnauthorizedAccessException("Only the owner of the chat can delete it.");
        
        context.Chats.Remove(entity);

        await context.SaveChangesAsync(cancellationToken);
    }
}