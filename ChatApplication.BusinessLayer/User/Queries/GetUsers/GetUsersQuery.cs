using ChatApplication.Database.Data.Models.Application;
using ChatApplication.Services.User.Queries.GetByIdUser;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.Services.User.Queries.GetUsers;

public record GetUsersQuery(): IRequest<List<GetByIdUserQueryResponse>>;

public class GetUsersQueryHandler(
    IApplicationDbContext context,
    IMapper mapper)
    : IRequestHandler<GetUsersQuery, List<GetByIdUserQueryResponse>>
{
    public async Task<List<GetByIdUserQueryResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        return await context.Users
            .Select(user => mapper.Map<GetByIdUserQueryResponse>(user))
            .ToListAsync(cancellationToken: cancellationToken);
    }
}