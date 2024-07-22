namespace ChatApplication.Services.Message.Queries.GetByIdMessage;

public class GetByIdMessageQueryResponse
{
    public string Text { get; init; }
    public uint OwnerId { get; init; }

    private GetByIdMessageQueryResponse()
    {
        Text = string.Empty;
        OwnerId = 0;
    }
    public GetByIdMessageQueryResponse(string text, uint ownerId)
    {
        Text = text;
        OwnerId = ownerId;
    }
}