using ChatApplication.Services.User.Commands.CreateUser;
using FluentValidation.TestHelper;
using Xunit;

namespace ChatApplication.BusinessLayer.UnitTests.User.Commands;

public partial class CreateUserCommandTesting
{
    [Fact]
    public void ValidateThrowShouldHaveExceptionNameIsEmpty()
    {
        var command = new CreateUserCommand(string.Empty);
        
        var result = _validator.TestValidate(command);
        
        result.ShouldHaveValidationErrorFor(c => c.Username);
        
    }
}