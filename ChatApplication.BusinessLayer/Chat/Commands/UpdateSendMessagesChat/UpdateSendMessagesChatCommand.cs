using Ardalis.GuardClauses;
using ChatApplication.database.Data.Models;
using ChatApplication.database.Data.Models.Application;
using MediatR;

namespace ChatApplication.Services.Chat.Commands.UpdateSendMessagesChat;

public record UpdateSendMessagesChatCommand(uint Id, database.Data.Models.Message SendMessages) : IRequest;

public class UpdateSendMessagesChatCommandHandler(IApplicationDbContext context) : IRequestHandler<UpdateSendMessagesChatCommand>
{
    public async Task Handle(UpdateSendMessagesChatCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.Chats
            .FindAsync(new object?[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.Messages.Add(request.SendMessages);

        await context.SaveChangesAsync(cancellationToken);
    }
}