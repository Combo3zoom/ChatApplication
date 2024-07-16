using System.Text.Json;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using System.Threading.Tasks;

namespace ChatApplication.Hub
{
    public interface IChatService
    {
        Task JoinChat(string connectionId, uint chatId, uint userId);
        Task SendMessage(string connectionId, string message);
        Task HandleDisconnect(string connectionId);
    }

    public class ChatService : IChatService
    {
        private readonly IDistributedCache _cache;
        private readonly IHubContext<ChatHub, IChatClient> _hubContext;

        public ChatService(IDistributedCache cache, IHubContext<ChatHub, IChatClient> hubContext)
        {
            _cache = cache;
            _hubContext = hubContext;
        }

        public async Task JoinChat(string connectionId, uint chatId, uint userId)
        {
            var connection = new UserConnection(connectionId, chatId, userId);
            
            await _hubContext.Groups.AddToGroupAsync(connection.ConnectionId, connection.ChatId.ToString());

            var stringConnection = JsonSerializer.Serialize(connection);

            await _cache.SetStringAsync(connection.ConnectionId, stringConnection);

            await _hubContext.Clients
                .Group(connection.ChatId.ToString())
                .ReceiveMessage("Admin", $"{connection.UserId} join to the chat");
        }

        public async Task SendMessage(string connectionId, string message)
        {
            var stringConnection = await _cache.GetStringAsync(connectionId);

            if (stringConnection != null)
            {
                var connection = JsonSerializer.Deserialize<UserConnection>(stringConnection);

                if (connection != null)
                {
                    await _hubContext.Clients
                        .Group(connection.ChatId.ToString())
                        .ReceiveMessage(connection.UserId.ToString(), message);
                }
            }
            
        }

        public async Task HandleDisconnect(string connectionId)
        {
            var stringConnection = await _cache.GetStringAsync(connectionId);
            var connection = JsonSerializer.Deserialize<UserConnection>(stringConnection);

            if (connection != null)
            {
                await _cache.RemoveAsync(connectionId);
                await _hubContext.Groups.RemoveFromGroupAsync(connectionId, connection.ChatId.ToString());

                await _hubContext.Clients
                    .Group(connection.ChatId.ToString())
                    .ReceiveMessage("Admin", $"{connection.UserId} leave the chat");
            }
        }
    }
}
