using ChatApplication.Database.Data.Models.Application;
using MediatR;

namespace ChatApplication.Services.User.Commands.CreateUser;

public record CreateUserCommand(string Username): IRequest<uint>;

public class CreateUserCommandHandler(IApplicationDbContext context) 
    : IRequestHandler<CreateUserCommand, uint>
{
    public async Task<uint> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new Database.Data.Models.User(default,
            request.Username, 
            Array.Empty<Database.Data.Models.Chat>(),
            Array.Empty<Database.Data.Models.Chat>()); 
        
        context.Users.Add(user);
        
        await context.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}