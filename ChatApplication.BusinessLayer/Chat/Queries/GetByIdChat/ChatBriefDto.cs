using AutoMapper;

namespace ChatApplication.Services.Chat.Queries.GetByIdChat;

public class ChatBriefDto
{
    public string Name { get; init; }
    public uint OwnerId { get; init; }

    private ChatBriefDto()
    {
        Name = string.Empty;
        OwnerId = 0;
    } 
    
    private class Mapping : Profile
    {
        public Mapping() => CreateMap<database.Data.Models.Chat, ChatBriefDto>();
    }
}