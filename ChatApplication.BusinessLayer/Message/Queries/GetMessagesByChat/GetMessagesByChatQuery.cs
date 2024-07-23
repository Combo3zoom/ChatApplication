using Ardalis.GuardClauses;
using ChatApplication.Database.Data.Models.Application;
using ChatApplication.Services.Chat.Queries.GetByUserIdChatsId;
using ChatApplication.Services.Message.Queries.GetByIdMessage;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.Services.Message.Queries.GetMessagesByChat;

public record GetByChatIdMessagesQuery(uint ChatId): IRequest<List<GetByIdMessageQueryResponse>>;

public class GetMessagesByChatQueryHandler(IApplicationDbContext context, IMapper mapper)
    : IRequestHandler<GetByChatIdMessagesQuery, List<GetByIdMessageQueryResponse>>
{
    public async Task<List<GetByIdMessageQueryResponse>> Handle(GetByChatIdMessagesQuery request, CancellationToken cancellationToken)
    {
        var chat =  context.Chats
            .Where(c => c.Id == request.ChatId)
            .Include(c => c.Messages)
            .Select(c => c.Messages);

        Guard.Against.NotFound(request.ChatId, chat);

        if (chat is null)
            throw new Exception("Chat doesn't exist");
        
        return await context.Messages
            .Select(message => mapper.Map<GetByIdMessageQueryResponse>(message))
            .ToListAsync(cancellationToken);
    }
}
