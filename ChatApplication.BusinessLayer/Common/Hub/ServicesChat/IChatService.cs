namespace ChatApplication.database.Services.Service;

public interface IChatService
{
    Task OnNewChatUserAdded(uint userId, uint chatId);
    Task CreateChatsMessagesSubscription(string connectionId, uint userId);
    Task SendMessage(uint userId, uint chatId, string message);
}