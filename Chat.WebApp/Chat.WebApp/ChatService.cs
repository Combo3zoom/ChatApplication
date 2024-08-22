namespace Chat.WebApp
{
    public class ChatService
    {
        private List<ChatItem> chatItems;

        public async Task<List<ChatItem>> GetChats(uint userId)
        {
            return chatItems.Where(chat => chat.OwnerId == userId).ToList();
        }

        public async Task<List<ChatItem>> GetChatsByName(uint userId, string chatName)
        {
            return chatItems.Where(chat => chat.Name.Contains(chatName, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public ChatService()
        {
            chatItems = new List<ChatItem>
            {
                new ChatItem { Id = 1, Name = "Chat with Alice", OwnerId = 101 },
                new ChatItem { Id = 2, Name = "Project Discussion", OwnerId = 102 },
                new ChatItem { Id = 3, Name = "Family Group", OwnerId = 101 },
                new ChatItem { Id = 4, Name = "Weekend Plans", OwnerId = 103 },
                new ChatItem { Id = 5, Name = "Work Chat", OwnerId = 104 },
                new ChatItem { Id = 6, Name = "Friends Reunion", OwnerId = 101 },
                new ChatItem { Id = 7, Name = "Study Group", OwnerId = 105 },
                new ChatItem { Id = 8, Name = "Gaming Buddies", OwnerId = 102 },
                new ChatItem { Id = 9, Name = "Shopping List", OwnerId = 103 },
                new ChatItem { Id = 10, Name = "Book Club", OwnerId = 104 },
                new ChatItem { Id = 11, Name = "Vacation Planning", OwnerId = 105 },
                new ChatItem { Id = 12, Name = "Movie Night", OwnerId = 101 },
                new ChatItem { Id = 13, Name = "Fitness Group", OwnerId = 102 },
                new ChatItem { Id = 14, Name = "Work Project", OwnerId = 103 },
                new ChatItem { Id = 15, Name = "Neighborhood Watch", OwnerId = 104 },
                new ChatItem { Id = 16, Name = "Photography Club", OwnerId = 105 },
                new ChatItem { Id = 17, Name = "Car Enthusiasts", OwnerId = 101 },
                new ChatItem { Id = 18, Name = "Tech Talk", OwnerId = 102 },
                new ChatItem { Id = 19, Name = "Cooking Recipes", OwnerId = 103 },
                new ChatItem { Id = 20, Name = "Pet Lovers", OwnerId = 104 },
                new ChatItem { Id = 21, Name = "Chat with Alice", OwnerId = 101 },
                new ChatItem { Id = 22, Name = "Chat with Alice", OwnerId = 101 },
                new ChatItem { Id = 23, Name = "Chat with Alice", OwnerId = 101 },
                new ChatItem { Id = 24, Name = "Chat with Alice", OwnerId = 101 },
                new ChatItem { Id = 25, Name = "Chat with Alice", OwnerId = 101 },
                new ChatItem { Id = 26, Name = "Chat with Alice", OwnerId = 101 },
                new ChatItem { Id = 27, Name = "Chat with Alice", OwnerId = 101 },
                new ChatItem { Id = 28, Name = "Chat with Alice", OwnerId = 101 },
                new ChatItem { Id = 28, Name = "Chat with Alice", OwnerId = 101 },
                new ChatItem { Id = 29, Name = "Chat with Alice", OwnerId = 101 },
                new ChatItem { Id = 30, Name = "Chat with Alice", OwnerId = 101 },
                new ChatItem { Id = 31, Name = "Chat with Alice", OwnerId = 101 },
                new ChatItem { Id = 32, Name = "Chat with Alice", OwnerId = 101 },
                new ChatItem { Id = 33, Name = "Chat with Alice", OwnerId = 101 },
                new ChatItem { Id = 34, Name = "Chat with Alice", OwnerId = 101 },
            };
        }
    }

    public class ChatItem
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public uint OwnerId { get; init; }
    }
}
