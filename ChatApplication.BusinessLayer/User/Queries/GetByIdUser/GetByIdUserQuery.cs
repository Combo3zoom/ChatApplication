using Ardalis.GuardClauses;
using ChatApplication.Database.Data.Models.Application;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.Services.User.Queries.GetByIdUser;

public record GetByIdUserQuery(uint Id) : IRequest<GetByIdUserQueryResponse>;

public class GetByIdUserQueryHandler(IApplicationDbContext context, IMapper mapper)
    : IRequestHandler<GetByIdUserQuery, GetByIdUserQueryResponse>
{
    public async Task<GetByIdUserQueryResponse> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
    {
        var userResponse = await context.Users
            .Where(user => user.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);;

        Guard.Against.NotFound(request.Id, userResponse);

        var transformedUserResponse = mapper.Map<GetByIdUserQueryResponse>(userResponse);
        
        if (transformedUserResponse is null)
            throw new NotFoundException(nameof(request.Id), nameof(context.Users));

        return await Task.FromResult(transformedUserResponse);
    }
}

