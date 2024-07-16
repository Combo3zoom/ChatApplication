using ChatApplication.Services.Chat.Commands.UpdateSendMessagesChat;
using FluentValidation;

namespace ChatApplication.Services.Chat.Commands.UpdateJoinedUsersChat;

public class UpdateJoinedUsersChatCommandValidator: AbstractValidator<UpdateJoinUserChatCommand>
{
    public UpdateJoinedUsersChatCommandValidator()
    {
        RuleFor(u => u.JoinUser)
            .NotEmpty();
    }
}