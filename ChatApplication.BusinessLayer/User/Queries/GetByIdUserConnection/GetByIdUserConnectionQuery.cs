using Ardalis.GuardClauses;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ChatApplication.Database.Data.Models.Application;
using MediatR;

namespace ChatApplication.Services.User.Queries.GetByIdUserConnection;

public record GetByIdUserConnectionQuery(uint UserId): IRequest<GetByIdUserConnectionQueryResponse>;

public class GetByIdUserConnectionQueryHandler(IApplicationDbContext context, IMapper mapper) 
    : IRequestHandler<GetByIdUserConnectionQuery, GetByIdUserConnectionQueryResponse>
{
    public Task<GetByIdUserConnectionQueryResponse> Handle(GetByIdUserConnectionQuery request, CancellationToken cancellationToken)
    {
        var userBriefDto = context.Users
            .Where(user => user.Id == request.UserId);
        
        var transformatedUserBriefDto = userBriefDto
            .ProjectTo<GetByIdUserConnectionQueryResponse>(mapper.ConfigurationProvider)
            .SingleOrDefault();
        
        if (transformatedUserBriefDto is null)
            throw new NotFoundException(nameof(request.UserId), nameof(context.Users));

        return Task.FromResult(transformatedUserBriefDto);
    }
}