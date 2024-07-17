namespace ChatApplication.database.Services.Hub;

public interface IChatHub
{
    public Task ReceiveMessage(string userName, string message);
}