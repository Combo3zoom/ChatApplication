using ChatApplication.database.Data.Models;
using ChatApplication.database.Data.Models.Application;
using MediatR;
namespace ChatApplication.Services.Chat.Commands.CreateChat;
public record CreateChatCommand(string Name, database.Data.Models.User Owner) : IRequest<uint>;

public class CreateChatCommandHandler(IApplicationDbContext context) : IRequestHandler<CreateChatCommand, uint>
{
    public async Task<uint> Handle(CreateChatCommand request, CancellationToken cancellationToken)
    {
        var entity = new database.Data.Models.Chat(request.Name, request.Owner.Id);
        entity.JoinedUsers.Add(request.Owner);
        
        context.Chats.Add(entity);

        await context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}