namespace ChatApplication.Controllers.Hub.OuterLayerChats;

public interface IChatHub
{
    public Task ReceiveMessage(string userName, string message);
}