using Ardalis.GuardClauses;
using ChatApplication.Database.Data.Models.Application;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.Services.Message.Queries.GetByIdMessage;

public record GetByIdMessageQuery(uint Id): IRequest<GetByIdMessageQueryResponse>;

public class GetByIdMessageQueryHandler(IApplicationDbContext context, IMapper mapper)
    : IRequestHandler<GetByIdMessageQuery, GetByIdMessageQueryResponse>
{
    public async Task<GetByIdMessageQueryResponse> Handle(GetByIdMessageQuery request, CancellationToken cancellationToken)
    {
        var messagesResponse = await context.Messages
            .Where(messages => messages.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        Guard.Against.NotFound(request.Id, messagesResponse);

        var transformatedmessagesResponse = mapper.Map<GetByIdMessageQueryResponse>(messagesResponse);
        
        if (transformatedmessagesResponse is null)
            throw new NotFoundException(nameof(request.Id), nameof(context.Messages));

        return await Task.FromResult(transformatedmessagesResponse);
    }
}