using AutoMapper;

namespace ChatApplication.Services.User.Queries.GetByIdUser;

public class UserBriefDto
{
    public string Name { get; init; }

    private UserBriefDto()
    {
        Name = string.Empty;
    } 
    
    private class Mapping : Profile
    {
        public Mapping() => CreateMap<database.Data.Models.User, UserBriefDto>();
    }
}