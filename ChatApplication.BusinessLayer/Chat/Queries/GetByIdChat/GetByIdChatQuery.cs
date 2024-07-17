using Ardalis.GuardClauses;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ChatApplication.Database.Data.Models.Application;
using MediatR;

namespace ChatApplication.Services.Chat.Queries.GetByIdChat;

public record GetByIdChatQuery(uint Id): IRequest<GetByIdChatQueryResponse>;

public class GetByIdChatQueryHandler(IApplicationDbContext context, IMapper mapper)
    : IRequestHandler<GetByIdChatQuery, GetByIdChatQueryResponse>
{
    public Task<GetByIdChatQueryResponse> Handle(GetByIdChatQuery request, CancellationToken cancellationToken)
    {
        var chatBriefDto = context.Chats
            .Where(chat => chat.Id == request.Id);
        
        var transformatedchatBriefDto = chatBriefDto
            .ProjectTo<GetByIdChatQueryResponse>(mapper.ConfigurationProvider)
            .SingleOrDefault();
        
        if (transformatedchatBriefDto is null)
            throw new NotFoundException(nameof(request.Id), nameof(context.Chats));

        return Task.FromResult(transformatedchatBriefDto);
    }
}