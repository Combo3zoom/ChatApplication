using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication.Services.Chat.Commands.SendChatMessage
{
    public class SendChatMessageResponse
    {
        public string Username { get; set; }

        public SendChatMessageResponse()
        {
            Username = string.Empty;
        }
        
        public SendChatMessageResponse(string usernName)
        {
            Username = usernName;
        }
    };
}
