using ChatApplication.database.Services.Hub;
using ChatApplication.database.Services.Service;
using Microsoft.AspNetCore.SignalR;

namespace ChatApplication.Database.Services.Hub;

public class ChatHub(IChatService chatService) : Hub<IChatHub>
{
    public async Task CreateChatsMessagesSubscription(uint userId)
    {
        await chatService.CreateChatsMessagesSubscription(Context.ConnectionId, userId);
    }
}