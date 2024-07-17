using AutoMapper;
using AutoMapper.QueryableExtensions;
using ChatApplication.Database.Data.Models.Application;
using ChatApplication.Services.Message.Queries.GetByIdMessage;
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
            .Include(c => c.Messages)
            .Where(c => c.Id == request.ChatId).Select(c => c.Messages);

        if (chat is null)
            throw new Exception("Chat doesn't exist");
        
        return await context.Messages
            .ProjectTo<GetByIdMessageQueryResponse>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
