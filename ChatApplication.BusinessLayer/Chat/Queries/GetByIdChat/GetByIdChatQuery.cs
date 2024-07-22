using Ardalis.GuardClauses;
using ChatApplication.Database.Data.Models.Application;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.Services.Chat.Queries.GetByIdChat;

public record GetByIdChatQuery(uint Id): IRequest<GetByIdChatQueryResponse>;

public class GetByIdChatQueryHandler(IApplicationDbContext context, IMapper mapper)
    : IRequestHandler<GetByIdChatQuery, GetByIdChatQueryResponse>
{
    public async Task<GetByIdChatQueryResponse> Handle(GetByIdChatQuery request, CancellationToken cancellationToken)
    {
        var chatResponse = await context.Chats
            .SingleOrDefaultAsync(chat => chat.Id == request.Id, cancellationToken: cancellationToken);

        Guard.Against.NotFound(request.Id, chatResponse);

        var transformatedchatResponse = mapper.Map<GetByIdChatQueryResponse>(chatResponse);
        
        if (transformatedchatResponse is null)
            throw new NotFoundException(nameof(request.Id), nameof(context.Chats));

        return await Task.FromResult(transformatedchatResponse);
    }
}