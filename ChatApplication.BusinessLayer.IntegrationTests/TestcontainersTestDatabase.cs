using System.Data.Common;
using ChatApplication.Database.Data;
using LightsOn.Application.IntegrationTests;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Respawn;
using Testcontainers.PostgreSql;

namespace ChatApplication.BusinessLayer.IntegrationTests;

public class TestcontainersTestDatabase : ITestDatabase
{
    private readonly PostgreSqlContainer  _container;
    private DbConnection _connection = null!;
    private string _connectionString = null!;
    private Respawner _respawner = null!;

    public TestcontainersTestDatabase()
    {
        _container = new PostgreSqlBuilder()
            .WithAutoRemove(true)
            .Build();
    }

    public async Task InitialiseAsync()
    {
        await _container.StartAsync();

        var connectionStringBuilder = new NpgsqlConnectionStringBuilder(_container.GetConnectionString())
        {
        };
        
        _connectionString = connectionStringBuilder.ToString();

        _connection = new NpgsqlConnection(_connectionString);
        await _connection.OpenAsync(CancellationToken.None);

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql(_connectionString)
            .Options;

        var context = new ApplicationDbContext(options);

        context.Database.Migrate();

        _respawner = await Respawner.CreateAsync(_connection, new RespawnerOptions
        {
            TablesToIgnore = new Respawn.Graph.Table[] { "__EFMigrationsHistory" },
            DbAdapter = DbAdapter.Postgres
        });
    }

    public DbConnection GetConnection()
    {
        return _connection;
    }

    public async Task ResetAsync()
    {
        await _respawner.ResetAsync(_connectionString);
    }

    public async Task DisposeAsync()
    {
        await _connection.DisposeAsync();
        await _container.DisposeAsync();
    }
}
