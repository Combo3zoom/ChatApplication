using ChatApplication.Database.Data;
using MediatR;

namespace ChatApplication.Services.Message.Commands.CreateMessage;

public record CreateMessageCommand(uint UserId, uint ChatId, string Message): IRequest<uint>;

public class CreateMessageCommandHandler(ApplicationDbContext context )
    : IRequestHandler<CreateMessageCommand, uint>
{
    public async Task<uint> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
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
