using Microsoft.AspNetCore.Mvc;
using ChatApplication.Hub;

namespace ChatApplication.Controllers
{
    [ApiController]
    public class ChatController(IChatService chatService) : ControllerBase
    {
        [HttpPost("api/chats/{chatId:int}/users/{userId:int}/join")]
        public async Task<IActionResult> JoinChat(
            [FromRoute] uint chatId,
            [FromRoute] uint userId)
        {
            await chatService.JoinChat("example-connection-id", chatId, userId);
            return Ok();
        }

        [HttpPost("api/chats/{chatId:int}/users/{userId:int}/send")]
        public async Task<IActionResult> SendMessage(
            [FromRoute] uint chatId,
            [FromRoute] uint userId,
            [FromBody] MessageDto messageDto)
        {
            await chatService.SendMessage("example-connection-id", messageDto.Message);
            return Ok();
        }
    }

    public class MessageDto(string message)
    {
        public string Message { get; set; } = message;
    }
}