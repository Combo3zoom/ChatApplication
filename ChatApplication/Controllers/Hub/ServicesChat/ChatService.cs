using ChatApplication.Controllers.Hub.OuterLayerChats;
using ChatApplication.Database.Data.Models.Application;
using ChatApplication.database.Services.Service;
using ChatApplication.Services.Chat.Commands.SendChatMessage;
using ChatApplication.Services.Chat.Queries.GetByUserIdChatsId;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace ChatApplication.Controllers.Hub.ServicesChat;

public class ChatService(IHubContext<ChatHub, IChatHub> hubContext, IApplicationDbContext context, ISender mediator)
    : IChatService
{
    public async Task OnNewChatUserAdded(uint userId, uint chatId)
    {
        await hubContext.Clients
            .Group(chatId.ToString())
            .ReceiveMessage("Admin", $"{userId} join to the chat");
    }
    
    public async Task CreateChatsMessagesSubscription(string connectionId, uint userId)
    {
        var chatsIdByUser = await mediator.Send(new GetChatsIdByUserIdQuery(userId));
        
        foreach (var chatId in chatsIdByUser)
            await hubContext.Groups.AddToGroupAsync(connectionId, chatId.Id.ToString());
    }

    public async Task SendMessage(uint userId, uint chatId, string message)
    {
        var username = await mediator.Send(new SendChatMessageCommand(chatId, userId, message));

        await hubContext.Clients
            .Group(chatId.ToString())
            .ReceiveMessage(username.Username, message);
    }
}