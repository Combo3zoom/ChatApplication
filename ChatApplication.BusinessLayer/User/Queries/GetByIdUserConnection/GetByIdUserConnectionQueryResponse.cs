using AutoMapper;

namespace ChatApplication.Services.User.Queries.GetByIdUserConnection;

public class GetByIdUserConnectionQueryResponse
{
    public string ConnectionId { get; init; }

    private GetByIdUserConnectionQueryResponse()
    {
        ConnectionId = string.Empty;
    } 
    
    private class Mapping : Profile
    {
        public Mapping() => CreateMap<Database.Data.Models.User, GetByIdUserConnectionQueryResponse>();
    }
}