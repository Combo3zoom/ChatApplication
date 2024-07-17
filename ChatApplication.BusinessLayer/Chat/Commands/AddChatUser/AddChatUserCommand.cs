using MediatR;

namespace ChatApplication.Services.Chat.Commands.AddChatUser;

public record AddChatUserCommand(uint ChatId, uint UserId)
    : IRequest;