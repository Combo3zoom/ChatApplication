namespace ChatApplication.database.Services;

public interface IMessageHubClient
{
    Task SendOffersToUser(List < string > message);
}