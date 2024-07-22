using ChatApplication.Database.Data.Models.Application;
using ChatApplication.Services.Chat.Queries.GetByIdChat;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.Services.Chat.Queries.GetChats;

public record GetChatsQuery(): IRequest<List<GetByIdChatQueryResponse>>;

public class GetChatsQueryHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetChatsQuery, List<GetByIdChatQueryResponse>>
{
    public async Task<List<GetByIdChatQueryResponse>> Handle(GetChatsQuery request, CancellationToken cancellationToken)
    {
        return await context.Chats
            .Select(chat => mapper.Map<GetByIdChatQueryResponse>(chat))
            .ToListAsync(cancellationToken);
    }
}