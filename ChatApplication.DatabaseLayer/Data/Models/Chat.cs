namespace ChatApplication.Database.Data.Models;

public class Chat
{
    public Chat(uint id, 
        string name, 
        uint ownerId, 
        User owner, 
        ICollection<Message> messages, 
        ICollection<User> joinedUsers)
    {
        Id = id;
        Name = name;
        OwnerId = ownerId;
        Owner = owner;
        Messages = messages;
        JoinedUsers = joinedUsers;
    }

    private Chat() { }
    public uint Id { get; private set; }
    public string Name { get; set; }
    public uint OwnerId { get; set; }
    public User Owner { get; private set; }
    public ICollection<Message> Messages { get; set; }
    public ICollection<User> JoinedUsers { get; private set; }
}