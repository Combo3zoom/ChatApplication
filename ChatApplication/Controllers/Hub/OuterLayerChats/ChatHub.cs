using ChatApplication.database.Services.Service;
using Microsoft.AspNetCore.SignalR;

namespace ChatApplication.Controllers.Hub.OuterLayerChats;

public class ChatHub(IChatService chatService) : Hub<IChatHub>
{
    public async Task CreateChatsMessagesSubscription(uint userId)
    {
        await chatService.CreateChatsMessagesSubscription(Context.ConnectionId, userId);
    }
}