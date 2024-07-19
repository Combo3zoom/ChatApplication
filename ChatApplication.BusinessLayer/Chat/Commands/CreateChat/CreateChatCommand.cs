using ChatApplication.database.Services.Service;
using ChatApplication.Database.Data.Models;
using ChatApplication.Database.Data.Models.Application;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.Services.Chat.Commands.CreateChat;
public record CreateChatCommand(string Name, uint UserId) : IRequest<uint>;
public record CreateChatBody(string Name) : IRequest<uint>;


public class CreateChatCommandHandler(IApplicationDbContext context, IChatService chatService)
    : IRequestHandler<CreateChatCommand, uint>
{
    public async Task<uint> Handle(CreateChatCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Users.Where(user => user.Id == request.UserId)
            .FirstOrDefaultAsync(cancellationToken);

        if (user == null)
            throw new Exception("User not found");

        var joinedUsers = new[] { user };
        
        var chat = new Database.Data.Models.Chat(default, 
            request.Name, 
            request.UserId,
            user,
            new List<Database.Data.Models.Message>(),
            joinedUsers);
        
        context.Chats.Add(chat);

        await context.SaveChangesAsync(cancellationToken);

        return chat.Id;
    }
}