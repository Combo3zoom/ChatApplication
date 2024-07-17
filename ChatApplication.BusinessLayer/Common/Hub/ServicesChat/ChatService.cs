using System.Text.Json;
using ChatApplication.Database.Data.Models.Application;
using ChatApplication.database.Services.Hub;
using ChatApplication.Database.Services.Hub;
using ChatApplication.database.Services.Service;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.Database.Services.Service;

public class ChatService(IHubContext<ChatHub, IChatHub> hubContext, IApplicationDbContext context)
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
        var chatIds = await context.Chats.Where(chat => chat.JoinedUsers.Any(user => user.Id == userId))
            .Select(chat => chat.Id)
            .ToArrayAsync();
        
        foreach (var chatId in chatIds)
        {
            await hubContext.Groups.AddToGroupAsync(connectionId, chatId.ToString());
        }
    }

    public async Task SendMessage(uint userId, uint chatId, string message)
    {
        var isChatMember = await context.Chats.Where(chat => chat.JoinedUsers.Any(user => user.Id == userId) && chat.Id == chatId)
            .Select(chat => chat.Id)
            .AnyAsync();

        if (!isChatMember)
            return;


        var username = await context.Users.Where(user => user.Id == userId).Select(user => user.Name).FirstOrDefaultAsync();
        
        await hubContext.Clients
            .Group(chatId.ToString())
            .ReceiveMessage(username, message);
    }
}