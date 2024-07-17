using ChatApplication.BusinessLayer.IntegrationTests;

namespace LightsOn.Application.IntegrationTests;

using static Testing;

public abstract class BaseTestFixture : IAsyncLifetime
{
    public async Task InitializeAsync()
    {
        await ResetState();
    }
    public Task DisposeAsync() => Task.CompletedTask;
}