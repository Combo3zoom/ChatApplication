namespace ChatApplication.Database.Data.Models;

public class Message
{
    public Message(uint id, uint ownerId, User owner, uint chatId, Chat chat, string text)
    {
        Id = id;
        OwnerId = ownerId;
        Owner = owner;
        ChatId = chatId;
        Chat = chat;
        Text = text;
    }

    private Message() { }
    public uint Id { get; set; }
    public uint OwnerId { get; init; }
    public User Owner { get; init; }
    public uint ChatId { get; init; }
    public Chat Chat { get; init; }
    public string Text { get; set; }
    
}