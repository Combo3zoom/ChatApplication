namespace ChatApplication.Database.Data.Models;

public class User
{
    public User(uint id, string name, ICollection<Chat> joinedChats, ICollection<Chat> createdChats)
    {
        Id = id;
        Name = name;
        JoinedChats = joinedChats;
        CreatedChats = createdChats;
    }

    private User() { }
    public uint Id { get; set; }
    public string Name { get; set; }
    public ICollection<Chat> JoinedChats { get; set; }
    public ICollection<Chat> CreatedChats { get; set; }
}