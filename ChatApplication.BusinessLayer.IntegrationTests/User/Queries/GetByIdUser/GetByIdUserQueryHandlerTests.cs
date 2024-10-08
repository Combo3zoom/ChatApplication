using ChatApplication.Database.Data.Models;
using Tynamix.ObjectFiller;

namespace ChatApplication.BusinessLayer.IntegrationTests.User.Queries.GetByIdUser;

[Collection("Tests")]
public partial class GetByIdUserQueryHandlerTests(Testing testing) : IClassFixture<Testing>
{
    private readonly Testing _testing = testing;

    public static IEnumerable<object[]> SExceptedUserTestCaseSource = new List<object[]>
    {
        new object[] { CreateRandomUser() }
    };
    public static IEnumerable<object[]> SExceptedUserTestCaseSourceIfUserNameIsEmpty = new List<object[]>
    {
        new object[] { 0 }
    };


    private static Database.Data.Models.User CreateRandomUser()
    {
        var user = new Database.Data.Models.User(default, string.Empty,
            new List<Chat>(),new List<Chat>());
        var filler = CreateUserFiller();
        filler.Fill(user);
        return user;
    }


    private static Filler<Database.Data.Models.User> CreateUserFiller()
    {
        var filler = new Filler<Database.Data.Models.User>();
        filler.Setup()
            .OnProperty(x => x.Id).IgnoreIt()
            .OnProperty(x => x.Name).Use(new RealNames(NameStyle.FirstName))
            .OnProperty(x => x.JoinedChats).IgnoreIt()
            .OnProperty(x => x.CreatedChats).IgnoreIt();
        

        return filler;
    }
}