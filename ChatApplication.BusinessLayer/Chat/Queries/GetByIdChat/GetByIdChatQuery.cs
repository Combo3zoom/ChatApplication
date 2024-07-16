using Ardalis.GuardClauses;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ChatApplication.database.Data.Models.Application;
using MediatR;

namespace ChatApplication.Services.Chat.Queries.GetByIdChat;

public record GetByIdChatQuery(uint Id): IRequest<ChatBriefDto>;

public class GetByIdChatQueryHandler(IApplicationDbContext context, IMapper mapper)
    : IRequestHandler<GetByIdChatQuery, ChatBriefDto>
{
    public Task<ChatBriefDto> Handle(GetByIdChatQuery request, CancellationToken cancellationToken)
    {
        var chatBriefDto = context.Chats
            .Where(chat => chat.Id == request.Id);
        
        var transformatedchatBriefDto = chatBriefDto
            .ProjectTo<ChatBriefDto>(mapper.ConfigurationProvider)
            .SingleOrDefault();
        
        if (transformatedchatBriefDto is null)
            throw new NotFoundException(nameof(request.Id), nameof(context.Chats));

        return Task.FromResult(transformatedchatBriefDto);
    }
}