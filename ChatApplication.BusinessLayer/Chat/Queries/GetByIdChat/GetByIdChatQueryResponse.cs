using AutoMapper;

namespace ChatApplication.Services.Chat.Queries.GetByIdChat;

public class GetByIdChatQueryResponse
{
    public string Name { get; init; }
    public uint OwnerId { get; init; }

    private GetByIdChatQueryResponse()
    {
        Name = string.Empty;
        OwnerId = 0;
    } 
    
    private class Mapping : Profile
    {
        public Mapping() => CreateMap<Database.Data.Models.Chat, GetByIdChatQueryResponse>();
    }
}