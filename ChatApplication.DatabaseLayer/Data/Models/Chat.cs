namespace ChatApplication.database.Data.Models;

public class Chat
{
    public uint Id { get; private set; }
    public string Name { get; set; }
    public uint OwnerId { get; set; }
    public User Owner { get; private set; }
    public ICollection<Message> Messages { get; set; }
    public ICollection<User> JoinedUsers { get; private set; }

    public Chat(string name, uint ownerId)
    {
        Name = name;
        OwnerId = ownerId;
        Messages = new List<Message>();
        JoinedUsers = new List<User>();
    }

    private Chat() { }
}