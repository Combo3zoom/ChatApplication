using Ardalis.GuardClauses;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ChatApplication.database.Data.Models.Application;
using MediatR;

namespace ChatApplication.Services.User.Queries.GetByIdUser;

public record GetByIdUserQuery(uint Id) : IRequest<UserBriefDto>;

public class GetByIdUserQueryHandler(IApplicationDbContext context, IMapper mapper)
    : IRequestHandler<GetByIdUserQuery, UserBriefDto>
{
    public Task<UserBriefDto> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
    {
        var userBriefDto = context.Users
            .Where(user => user.Id == request.Id);
        
        var transformateduserBriefDto = userBriefDto
            .ProjectTo<UserBriefDto>(mapper.ConfigurationProvider)
            .SingleOrDefault();
        
        if (transformateduserBriefDto is null)
            throw new NotFoundException(nameof(request.Id), nameof(context.Chats));

        return Task.FromResult(transformateduserBriefDto);
    }
}

