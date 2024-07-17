using Ardalis.GuardClauses;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ChatApplication.Database.Data.Models.Application;
using MediatR;

namespace ChatApplication.Services.User.Queries.GetByIdUser;

public record GetByIdUserQuery(uint Id) : IRequest<GetByIdUserQueryResponse>;

public class GetByIdUserQueryHandler(IApplicationDbContext context, IMapper mapper)
    : IRequestHandler<GetByIdUserQuery, GetByIdUserQueryResponse>
{
    public Task<GetByIdUserQueryResponse> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
    {
        var userBriefDto = context.Users
            .Where(user => user.Id == request.Id);
        
        var transformatedUserBriefDto = userBriefDto
            .ProjectTo<GetByIdUserQueryResponse>(mapper.ConfigurationProvider)
            .SingleOrDefault();
        
        if (transformatedUserBriefDto is null)
            throw new NotFoundException(nameof(request.Id), nameof(context.Users));

        return Task.FromResult(transformatedUserBriefDto);
    }
}

