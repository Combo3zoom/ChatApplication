using System.Text.Json;
using ChatApplication.Database.Data.Models.Application;
using ChatApplication.database.Services.Hub;
using ChatApplication.Database.Services.Hub;
using ChatApplication.database.Services.Service;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ChatApplication.Services.Message.Queries.GetMessagesByChat;
using MediatR;
using ChatApplication.Services.Chat.Commands.UpdateSendMessagesChat;
using ChatApplication.BusinessLayer.IntegrationTests.User.Queries.GetByUserIdChatsId;

namespace ChatApplication.Database.Services.Service;

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
        //var chatsId = await context.Chats.Where(chat => chat.JoinedUsers.Any(user => user.Id == userId))
        //    .Select(chat => chat.Id)
        //    .ToArrayAsync();

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