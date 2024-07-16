namespace ChatApplication.database.Services;
using Microsoft.AspNetCore.SignalR;

public class MessageHubClient: Hub<IMessageHubClient> {
    public async Task SendOffersToUser(List < string > message) {
        await Clients.All.SendOffersToUser(message);
    }
}