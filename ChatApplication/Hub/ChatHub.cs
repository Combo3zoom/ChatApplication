using ChatApplication;
using ChatApplication.Hub;
using Microsoft.AspNetCore.SignalR;

public interface IChatClient
{
    public Task ReceiveMessage(string userName, string message);
}


public class ChatHub(IChatService chatService) : Hub<IChatClient>
{
    public async Task JoinChat(string connectionId, uint chatId, uint userId)
    {
        await chatService.JoinChat("example-connection-id", chatId, userId);
    }

    public async Task SendMessage(string message)
    {
        await chatService.SendMessage("example-connection-id", message);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await chatService.HandleDisconnect(Context.ConnectionId);
        await base.OnDisconnectedAsync(exception);
    }
}

