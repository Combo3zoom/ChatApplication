using Ardalis.GuardClauses;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ChatApplication.Database.Data.Models.Application;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.Services.Chat.Queries.GetByIdChat;

public record GetByIdChatQuery(uint Id): IRequest<GetByIdChatQueryResponse>;

public class GetByIdChatQueryHandler(IApplicationDbContext context, IMapper mapper)
    : IRequestHandler<GetByIdChatQuery, GetByIdChatQueryResponse>
{
    public Task<GetByIdChatQueryResponse> Handle(GetByIdChatQuery request, CancellationToken cancellationToken)
    {
        var chatResponse = context.Chats
            .Where(chat => chat.Id == request.Id);

        Guard.Against.NotFound(request.Id, chatResponse);

        var transformatedchatResponse = chatResponse
            .ProjectTo<GetByIdChatQueryResponse>(mapper.ConfigurationProvider)
            .SingleOrDefault();
        
        if (transformatedchatResponse is null)
            throw new NotFoundException(nameof(request.Id), nameof(context.Chats));

        return Task.FromResult(transformatedchatResponse);
    }
}