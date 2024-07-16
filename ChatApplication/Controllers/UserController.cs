using ChatApplication.Services.User.Commands.CreateUser;
using ChatApplication.Services.User.Queries.GetByIdUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChatApplication.Controllers;

// [ApiController]
// public class UserController: ControllerBase
// {
//     public async Task<UserBriefDto> GetByIdUser(ISender sender, uint id)
//     {
//         return await sender.Send(new GetByIdUserQuery(id));
//     }
//
//     public async Task<uint> CreateUser(ISender sender, CreateUserCommand command)
//     {
//         return await sender.Send(command);
//     }
//         
// }