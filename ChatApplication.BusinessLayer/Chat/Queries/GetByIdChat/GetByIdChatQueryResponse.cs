namespace ChatApplication.Services.Chat.Queries.GetByIdChat;

public class GetByIdChatQueryResponse
{
    public uint Id { get; init; }
    public string Name { get; init; }
    public uint OwnerId { get; init; }

    private GetByIdChatQueryResponse()
    {
        Name = string.Empty;
        OwnerId = 0;
    } 
    
    public GetByIdChatQueryResponse(uint id, string name, uint owner)
    {
        Id = id;
        Name = name;
        OwnerId = owner;
    } 
}