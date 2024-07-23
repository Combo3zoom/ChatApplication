using ChatApplication.Services.User.Commands.CreateUser;
using ChatApplication.Services.User.Queries.GetByIdUser;
using ChatApplication.Services.User.Queries.GetUsers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChatApplication.Controllers;

[ApiController]
public class UserController(ISender mediator) : ControllerBase
{

    [HttpPost("api/users")]
    public async Task<ActionResult<CreatedAtActionResult>> CreateUser(CreateUserCommand command)
    {
        var userId = await mediator.Send(command);
        return CreatedAtAction(nameof(GetByIdUser), new { id = userId }, userId);
    }
    
    [HttpGet("api/users/{id}")]
    public async Task<ActionResult<GetByIdUserQueryResponse>> GetByIdUser(uint id)
    {
        return Ok(await mediator.Send(new GetByIdUserQuery(id)));
    }
    
    [HttpGet("api/users")]
    public async Task<ActionResult<GetByIdUserQueryResponse>> GetUsers()
    {
        return Ok(await mediator.Send(new GetUsersQuery()));
    }
}