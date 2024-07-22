using ChatApplication.Services.Chat.Queries.GetByIdChat;

namespace ChatApplication.Services.Chat.Queries.GetByUserIdChatsId;

public class GetChatsIdByUserIdQueryResponse
{
    public uint Id { get; init; }

    private GetChatsIdByUserIdQueryResponse()
    {
        Id = 0;
    }
    public GetChatsIdByUserIdQueryResponse(uint id)
    {
        Id = id;
    }
}