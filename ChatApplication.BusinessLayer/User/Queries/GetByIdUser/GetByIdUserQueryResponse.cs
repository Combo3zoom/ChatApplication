namespace ChatApplication.Services.User.Queries.GetByIdUser;

public class GetByIdUserQueryResponse
{
    public string Name { get; init; }
    
    private GetByIdUserQueryResponse()
    {
        Name = string.Empty;
    }

    public GetByIdUserQueryResponse(string name)
    {
        Name = name;
    }
}