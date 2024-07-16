namespace ChatApplication.database.Data.Models;

public class User
{
    public uint Id { get; set; }
    public string Name { get; set; }
    public ICollection<Chat> JoinedChats { get; set; }
    public ICollection<Chat> CreatedChats { get; set; }

    public User(string name)
    {
        this.Name = name;
        JoinedChats = new List<Chat>();
        CreatedChats = new List<Chat>();
    }

    private User() { }
}