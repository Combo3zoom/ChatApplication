using AutoMapper;

namespace ChatApplication.Services.Message.Queries.GetByIdMessage;

public class GetByIdMessageQueryResponse
{
    public string text { get; init; }
    public uint OwnerId { get; init; }

    private GetByIdMessageQueryResponse()
    {
        text = string.Empty;
        OwnerId = 0;
    } 
    
    private class Mapping : Profile
    {
        public Mapping() => CreateMap<Database.Data.Models.Message, GetByIdMessageQueryResponse>();
    }
}