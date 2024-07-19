using Ardalis.GuardClauses;
using ChatApplication.Database.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.Services.Message.Commands.CreateMessage;

public record CreateMessageCommand(uint UserId, uint ChatId, string Message): IRequest<uint>;

public class CreateMessageCommandHandler(ApplicationDbContext context )
    : IRequestHandler<CreateMessageCommand, uint>
{
    public async Task<uint> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        var chat = await context.Chats.FirstOrDefaultAsync(chat => chat.Id == request.ChatId, cancellationToken);
        var user = await context.Users.FirstOrDefaultAsync(user => user.Id == request.UserId, cancellationToken);

        Guard.Against.NotFound(request.ChatId, chat);
        Guard.Against.NotFound(request.UserId, user);

        var message = new Database.Data.Models.Message(default,
            request.UserId,
            null,
            request.ChatId,
            null,
            request.Message);
        
        context.Messages.Add(message);

        await context.SaveChangesAsync(cancellationToken);
        
        return message.Id;
    }
}
