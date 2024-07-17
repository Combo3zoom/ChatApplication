using ChatApplication.Services.User.Commands.CreateUser;

namespace ChatApplication.BusinessLayer.IntegrationTests.User.Commands.CreateUser;

public partial class CreateUserCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(SExceptedUserTestCaseSource))]
    public async Task ShouldCreateUser(Database.Data.Models.User exceptedUser)
    {
        var createUserCommand = new CreateUserCommand(exceptedUser.Name);
        var userId = await testing.SendAsync(createUserCommand);
        var user = await testing.FindAsync<Database.Data.Models.User>(userId);

        user.Should().NotBeNull();
        user!.Name.Should().Be(exceptedUser.Name);
    }
}