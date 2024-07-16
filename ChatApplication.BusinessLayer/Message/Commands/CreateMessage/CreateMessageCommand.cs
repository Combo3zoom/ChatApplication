using ChatApplication.database.Data;
using MediatR;

namespace ChatApplication.Services.Message.Commands.CreateMessage;

public record CreateMessageCommand(string Message, uint UserId, uint ChatId): IRequest<uint>;

public class CreateMessageCommandHandler(ApplicationDbContext context )
    : IRequestHandler<CreateMessageCommand, uint>
{
    public async Task<uint> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        var entity = new database.Data.Models.Message(request.Message, request.UserId, request.ChatId);
        
        context.Messages.Add(entity);

        await context.SaveChangesAsync(cancellationToken);
        
        return entity.Id;
    }
}
