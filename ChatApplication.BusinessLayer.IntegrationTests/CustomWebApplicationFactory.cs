using System.Data.Common;
using ChatApplication.Database.Data;
using ChatApplication.database.Services.Hub;
using LightsOn.Application.IntegrationTests;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Identity.Client;
using ChatApplication;
using LightsOn.WebApi;

namespace ChatApplication.BusinessLayer.IntegrationTests;

using static Testing;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly DbConnection _connection;
    private readonly Mock<IChatHub> _iChatHub;

    public CustomWebApplicationFactory(DbConnection connection, 
        Mock<IChatHub> iChatHub)
    {
        _connection = connection;
        _iChatHub = iChatHub;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services
                .RemoveAll<IChatHub>()
                .AddScoped<IChatHub>(provider => _iChatHub.Object);
            services
                .RemoveAll<DbContextOptions<ApplicationDbContext>>()
                .AddDbContext<ApplicationDbContext>((sp, options) =>
                {
                    options.UseNpgsql(_connection);
                });
        });
    }
}
