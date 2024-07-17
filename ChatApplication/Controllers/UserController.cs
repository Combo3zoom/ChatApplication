using ChatApplication.Services.User.Commands.CreateUser;
using ChatApplication.Services.User.Queries.GetByIdUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChatApplication.Controllers;

[ApiController]
public class UserController(ISender mediator) : ControllerBase
{
    private readonly ISender _mediator = mediator;

    [HttpGet("api/users/{id}")]
    public async Task<GetByIdUserQueryResponse> GetByIdUser(uint id)
    {
        return await mediator.Send(new GetByIdUserQuery(id));
    }

    [HttpPost("api/users/create")]
    public async Task<CreatedAtActionResult> CreateUser(CreateUserCommand command)
    {
        var userId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetByIdUser), new { id = userId }, userId);
    }
        
}