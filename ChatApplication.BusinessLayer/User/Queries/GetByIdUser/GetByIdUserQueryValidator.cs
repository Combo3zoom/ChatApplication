using FluentValidation;

namespace ChatApplication.Services.User.Queries.GetByIdUser;

public class GetByIdUserQueryValidator: AbstractValidator<GetByIdUserQuery>
{
    public GetByIdUserQueryValidator()
    {
        RuleFor(u => u.Id)
            .NotEmpty();
    }
}