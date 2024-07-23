namespace ChatApplication.Services.User.Queries.GetByIdUser;

public class GetByIdUserQueryResponse
{
    public uint Id { get; init; }
    public string Name { get; init; }
    
    private GetByIdUserQueryResponse()
    {
        Id = 0;
        Name = string.Empty;
    }

    public GetByIdUserQueryResponse(uint id, string name)
    {
        Id = id;
        Name = name;
    }
}