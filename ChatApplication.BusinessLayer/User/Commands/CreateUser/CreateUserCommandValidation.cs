using FluentValidation;

namespace ChatApplication.Services.User.Commands.CreateUser;

public class CreateUserCommandValidation : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidation()
    {
        RuleFor(v => v.Username)
            .NotEmpty();
    }
}