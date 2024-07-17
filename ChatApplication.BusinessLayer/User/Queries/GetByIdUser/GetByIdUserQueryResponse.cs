using AutoMapper;

namespace ChatApplication.Services.User.Queries.GetByIdUser;

public class GetByIdUserQueryResponse
{
    public string Name { get; init; }

    private GetByIdUserQueryResponse()
    {
        Name = string.Empty;
    } 
    
    private class Mapping : Profile
    {
        public Mapping() => CreateMap<Database.Data.Models.User, GetByIdUserQueryResponse>();
    }
}