using FluentValidation;

namespace ChatApplication.Services.Chat.Commands.CreateChat;

public class CreateChatCommandValidator: AbstractValidator<CreateChatCommand>
{
    public CreateChatCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty()
            .MaximumLength(20);
    }
}