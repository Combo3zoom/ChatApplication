using ChatApplication.database.Data.Models.Application;
using MediatR;

namespace ChatApplication.Services.User.Commands.CreateUser;

public record CreateUserCommand(string Name): IRequest<uint>;

public class CreateUserCommandHandler(IApplicationDbContext context) 
    : IRequestHandler<CreateUserCommand, uint>
{
    public async Task<uint> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = new database.Data.Models.User(request.Name); 
        
        context.Users.Add(entity);
        
        await context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}