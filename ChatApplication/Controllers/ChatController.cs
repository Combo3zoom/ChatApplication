using ChatApplication.Services.Chat.Commands.AddChatUser;
using ChatApplication.Services.Chat.Commands.CreateChat;
using ChatApplication.Services.Chat.Commands.DeleteChat;
using ChatApplication.Services.Chat.Commands.SendChatMessage;
using ChatApplication.Services.Chat.Queries.GetByIdChat;
using ChatApplication.Services.Chat.Queries.GetChats;
using ChatApplication.Services.Message.Queries.GetMessagesByChat;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChatApplication.Controllers
{
    [ApiController]
    public class ChatController(ISender mediator) : ControllerBase
    {
        [HttpPost("api/users/{userId:int}/chats")]
        public async Task<ActionResult<uint>> CreateChat(
            [FromRoute] uint userId, 
            [FromBody] CreateChatBody body)
        {
            var chatId = await mediator
                .Send(new CreateChatCommand(body.Name, userId));
            
            return CreatedAtAction(nameof(GetByIdChat), new { id = chatId }, chatId);
        }

        [HttpDelete("api/users/{userId:int}/chats/{chatId:int}")]
        public async Task<IActionResult> DeleteChat(
            [FromRoute] uint userId, 
            [FromRoute] uint chatId)
        {
            await mediator.Send(new DeleteChatCommand(userId, chatId));
            
            return NoContent();
        }

        [HttpPut("api/users/{userId:int}/chats/{chatId:int}/join")]
        public async Task<IActionResult> AddChatUser(
            [FromRoute] uint chatId, 
            [FromRoute] uint userId)
        {
            await mediator.Send(new AddChatUserCommand(chatId, userId));
            
            return NoContent();
        }

        [HttpPost("api/users/{userId:int}/chats/{chatId:int}/messages")]
        public async Task<IActionResult> SendChatMessage(
            [FromRoute] uint chatId,
            [FromRoute] uint userId,
            [FromBody] SendChatMessageBody body)
        {
            await mediator.Send(new SendChatMessageCommand(chatId, userId, body.Message));
            return NoContent();
        }

        [HttpGet("api/chats/{id:int}")]
        public async Task<ActionResult<GetByIdChatQueryResponse>> GetByIdChat(
            [FromRoute] uint id)
        {
            return Ok(await mediator.Send(new GetByIdChatQuery(id)));
        }
        
        [HttpGet("api/chats/{id:int}/messages")]
        public async Task<ActionResult<GetByIdChatQueryResponse>> GetByChatIdMessages(
            [FromRoute] uint id)
        {
            return Ok(await mediator.Send(new GetByChatIdMessagesQuery(id)));
        }
        
        [HttpGet("api/chats")]
        public async Task<ActionResult<GetByIdChatQueryResponse>> GetChats()
        {
            return Ok(await mediator.Send(new GetChatsQuery()));
        }
    }
}