using AutoMapper;

namespace ChatApplication.Services.Message.Queries.GetByIdMessage;

public class MessageBriefDto
{
    public string text { get; init; }
    public uint OwnerId { get; init; }

    private MessageBriefDto()
    {
        text = string.Empty;
        OwnerId = 0;
    } 
    
    private class Mapping : Profile
    {
        public Mapping() => CreateMap<database.Data.Models.Message, MessageBriefDto>();
    }
}