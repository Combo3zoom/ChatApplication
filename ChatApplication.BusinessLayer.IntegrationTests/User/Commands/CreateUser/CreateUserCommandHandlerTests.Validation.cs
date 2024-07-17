using ChatApplication.Services.User.Commands.CreateUser;
using FluentValidation;
using FluentValidation.TestHelper;

namespace ChatApplication.BusinessLayer.IntegrationTests.User.Commands.CreateUser;

public partial class CreateUserCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(SExceptedUserTestCaseSourceIfUserNameIsEmpty))]
    public async Task ShouldThrowNullExceptionOnCreateIfClientNameIsEmpty(string userName)
    {
        var nonExistedUser = new CreateUserCommand(userName);

        await FluentActions.Invoking(() =>
            testing.SendAsync(nonExistedUser)).Should().ThrowAsync<ValidationException>();
    }
}