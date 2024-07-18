using Ardalis.GuardClauses;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ChatApplication.Database.Data.Models;
using ChatApplication.Database.Data.Models.Application;
using ChatApplication.Services.Message.Queries.GetByIdMessage;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.BusinessLayer.IntegrationTests.User.Queries.GetByUserIdChatsId
{
    public record GetChatsIdByUserIdQuery(uint UserId) : IRequest<List<GetChatsIdByUserIdQueryResponse>>;

    public class GetChatsIdByUserIdQueryHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetChatsIdByUserIdQuery, List<GetChatsIdByUserIdQueryResponse>>
    {
        public async Task<List<GetChatsIdByUserIdQueryResponse>> Handle(GetChatsIdByUserIdQuery request, CancellationToken cancellationToken)
        {
            var actualUserId = await context.Users
                .Select(u => u.Id)
                .FirstOrDefaultAsync(userId => userId == request.UserId);

            Guard.Against.NotFound(request.UserId, actualUserId);

            return await context.Chats
                .Where(chat => chat.JoinedUsers.Any(u => u.Id == actualUserId))
                .Select(chat => chat.Id)
                .ProjectTo<GetChatsIdByUserIdQueryResponse>(mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
