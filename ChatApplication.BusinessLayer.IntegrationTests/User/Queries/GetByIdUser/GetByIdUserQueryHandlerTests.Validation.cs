using ChatApplication.Services.User.Queries.GetByIdUser;
using FluentValidation;

namespace ChatApplication.BusinessLayer.IntegrationTests.User.Queries.GetByIdUser;

public partial class GetByIdUserQueryHandlerTests
{
    [Theory]
    [MemberData(nameof(SExceptedUserTestCaseSourceIfUserNameIsEmpty))]
    public async Task ShouldThrowNotFoundExceptionIfUserIdNonExist(
        uint incorrectUserId)
    {
        var nonExistedUser = new GetByIdUserQuery(incorrectUserId);
        
        await FluentActions.Invoking(() => _testing.SendAsync(nonExistedUser))
            .Should().ThrowAsync<ValidationException>();
    }
}