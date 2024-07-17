using FluentValidation;

namespace ChatApplication.Services.Chat.Commands.AddChatUser;

public class AddChatUserCommandValidator: AbstractValidator<AddChatUserCommand>
{
    public AddChatUserCommandValidator()
    {
        RuleFor(u => u.UserId)
            .NotEmpty();
    }
}