using Ardalis.GuardClauses;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ChatApplication.Database.Data.Models;
using ChatApplication.Database.Data.Models.Application;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.Services.User.Queries.GetByIdUser;

public record GetByIdUserQuery(uint Id) : IRequest<GetByIdUserQueryResponse>;

public class GetByIdUserQueryHandler(IApplicationDbContext context, IMapper mapper)
    : IRequestHandler<GetByIdUserQuery, GetByIdUserQueryResponse>
{
    public Task<GetByIdUserQueryResponse> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
    {
        var userResponse = context.Users
            .Where(user => user.Id == request.Id);

        Guard.Against.NotFound(request.Id, userResponse);

        var transformatedUserResponse = userResponse
            .ProjectTo<GetByIdUserQueryResponse>(mapper.ConfigurationProvider)
            .SingleOrDefault();
        
        if (transformatedUserResponse is null)
            throw new NotFoundException(nameof(request.Id), nameof(context.Users));

        return Task.FromResult(transformatedUserResponse);
    }
}

