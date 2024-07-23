using ChatApplication.Database.Data.Models.Application;
using ChatApplication.Services.User.Commands.CreateUser;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace ChatApplication.BusinessLayer.UnitTests.User.Commands;

public partial class CreateUserCommandTesting
{
    private readonly CreateUserCommandValidator _validator;
    private readonly Mock<IApplicationDbContext> _mockContext;
    private readonly Mock<ILogger<CreateUserCommandHandler>> _mockLogger;
    private readonly CreateUserCommandHandler _handler;

    public CreateUserCommandTesting()
    {
        _mockContext = new Mock<IApplicationDbContext>();
        _mockLogger = new Mock<ILogger<CreateUserCommandHandler>>();
        
        var mockDbSet = CreateMockDbSet();
        _mockContext.Setup(m => m.Users).Returns(mockDbSet.Object);

        _handler = new CreateUserCommandHandler(_mockContext.Object, _mockLogger.Object);
        _validator = new CreateUserCommandValidator();
    }
    
    private static Mock<DbSet<ChatApplication.Database.Data.Models.User>> CreateMockDbSet()
    {
        var queryable = new List<ChatApplication.Database.Data.Models.User>().AsQueryable();
        var mockDbSet = new Mock<DbSet<ChatApplication.Database.Data.Models.User>>();

        mockDbSet.As<IQueryable<ChatApplication.Database.Data.Models.User>>().Setup(m => m.Provider).
            Returns(queryable.Provider);
        mockDbSet.As<IQueryable<ChatApplication.Database.Data.Models.User>>().Setup(m => m.Expression)
            .Returns(queryable.Expression);
        mockDbSet.As<IQueryable<ChatApplication.Database.Data.Models.User>>().Setup(m => m.ElementType)
            .Returns(queryable.ElementType);
        mockDbSet.As<IQueryable<ChatApplication.Database.Data.Models.User>>().Setup(m => m.GetEnumerator())
            .Returns(queryable.GetEnumerator());

        return mockDbSet;
    }
}