namespace ChatApplication.database.Data.Models;

public class Message
{
    public uint Id { get; set; }
    public uint OwnerId { get; init; }
    public User Owner { get; init; }
    public uint ChatId { get; init; }
    public Chat Chat { get; init; }
    public string Text { get; set; }

    public Message(string text, uint ownerId, uint chatId)
    {
        OwnerId = ownerId;
        ChatId = chatId;
        Text = text;
    }

    private Message() { }
}