using ChatApplication.Services.User.Commands.CreateUser;
using ChatApplication.Services.User.Queries.GetByIdUser;

namespace ChatApplication.BusinessLayer.IntegrationTests.User.Queries.GetByIdUser;

public partial class GetByIdUserQueryHandlerTests
{
    [Theory]
    [MemberData(nameof(SExceptedUserTestCaseSource))]
    public async Task ShouldGetByIdUser(Database.Data.Models.User randomClient)
    {
        var createUserCommand = new CreateUserCommand(randomClient.Name);
        var userId = await _testing.SendAsync(createUserCommand);
        
        var user = await _testing.FindAsync<Database.Data.Models.User>(userId);
        
        var getByIdUserQuery = new GetByIdUserQuery(userId);
        var actualUser = await _testing.SendAsync(getByIdUserQuery);

        user.Should().NotBeNull();
        user!.Name.Should().Be(actualUser.Name);
    }
}