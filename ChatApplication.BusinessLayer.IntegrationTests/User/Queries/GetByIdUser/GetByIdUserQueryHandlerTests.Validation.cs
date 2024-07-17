using ChatApplication.Services.User.Queries.GetByIdUser;
using FluentValidation;

namespace ChatApplication.BusinessLayer.IntegrationTests.User.Queries.GetByIdUser;

public partial class GetByIdUserQueryHandlerTests
{
    [Theory]
    [MemberData(nameof(SExceptedUserTestCaseSourceIfUserNameIsEmpty))]
    public async Task ShouldThrowNotFoundExceptionIfUserIdNonExist(
        Database.Data.Models.User incorrectUser)
    {
        var nonExistedUser = new GetByIdUserQuery(incorrectUser.Id);
        
        await FluentActions.Invoking(() => _testing.SendAsync(nonExistedUser))
            .Should().ThrowAsync<ValidationException>();
    }
}