using Ardalis.GuardClauses;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ChatApplication.Database.Data.Models;
using ChatApplication.Database.Data.Models.Application;
using MediatR;

namespace ChatApplication.Services.Message.Queries.GetByIdMessage;

public record GetByIdMessageQuery(uint Id): IRequest<GetByIdMessageQueryResponse>;

public class GetByIdMessageQueryHandler(IApplicationDbContext context, IMapper mapper)
    : IRequestHandler<GetByIdMessageQuery, GetByIdMessageQueryResponse>
{
    public Task<GetByIdMessageQueryResponse> Handle(GetByIdMessageQuery request, CancellationToken cancellationToken)
    {
        var messagesResponse = context.Messages
            .Where(messages => messages.Id == request.Id);

        Guard.Against.NotFound(request.Id, messagesResponse);

        var transformatedmessagesResponse = messagesResponse
            .ProjectTo<GetByIdMessageQueryResponse>(mapper.ConfigurationProvider)
            .SingleOrDefault();
        
        if (transformatedmessagesResponse is null)
            throw new NotFoundException(nameof(request.Id), nameof(context.Messages));

        return Task.FromResult(transformatedmessagesResponse);
    }
}