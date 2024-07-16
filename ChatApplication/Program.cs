using ChatApplication.database;
using ChatApplication.Hub;
using ChatApplication.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddStackExchangeRedisCache(option =>
{
    var connection = builder.Configuration.GetConnectionString("Redis");
    option.Configuration = connection;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddControllers();

builder.Services.AddBusinessLayerServices();
builder.Services.AddDataBaseLayerServices(builder.Configuration);

builder.Services.AddSingleton<IChatService, ChatService>();
builder.Services.AddSingleton<ChatHub>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapHub<ChatHub>("/chat");
app.MapControllers();

app.Run();