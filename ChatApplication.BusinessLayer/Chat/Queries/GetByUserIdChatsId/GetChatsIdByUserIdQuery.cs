using Ardalis.GuardClauses;
using ChatApplication.Database.Data.Models.Application;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.Services.Chat.Queries.GetByUserIdChatsId
{
    public record GetChatsIdByUserIdQuery(uint UserId) : IRequest<List<GetChatsIdByUserIdQueryResponse>>;

    public class GetChatsIdByUserIdQueryHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetChatsIdByUserIdQuery, List<GetChatsIdByUserIdQueryResponse>>
    {
        public async Task<List<GetChatsIdByUserIdQueryResponse>> Handle(GetChatsIdByUserIdQuery request, CancellationToken cancellationToken)
        {
            var actualUserId = await context.Users
                .Select(u => u.Id)
                .FirstOrDefaultAsync(userId => userId == request.UserId, cancellationToken: cancellationToken);

            Guard.Against.NotFound(request.UserId, actualUserId);

            return await context.Chats
                .Where(chat => chat.JoinedUsers.Any(u => u.Id == actualUserId))
                .Select(message => mapper.Map<GetChatsIdByUserIdQueryResponse>(message))
                .ToListAsync(cancellationToken);
            
        }
    }
}
