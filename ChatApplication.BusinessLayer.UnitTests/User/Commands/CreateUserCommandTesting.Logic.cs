using ChatApplication.Services.User.Commands.CreateUser;
using Moq;
using Xunit;

namespace ChatApplication.BusinessLayer.UnitTests.User.Commands;

public partial class CreateUserCommandTesting
{
    [Fact]
    public async Task CreateUserHandlerTests()
    {
        var firstRequest = new CreateUserCommand("TestUser");
        
        var result = await _handler.Handle(firstRequest, new CancellationToken());
        
        _mockContext.Verify(m => m.Users.Add(It.IsAny<ChatApplication.Database.Data.Models.User>()), Times.Once);
        
        Assert.True(result == 0);
    }
}