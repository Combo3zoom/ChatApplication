using ChatApplication.Controllers.Hub.OuterLayerChats;
using ChatApplication.Controllers.Hub.ServicesChat;
using ChatApplication.Database;
using ChatApplication.database.Services.Service;
using ChatApplication.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddControllers();

builder.Services.AddBusinessLayerServices();
builder.Services.AddDatabaseLayerServices(builder.Configuration);

builder.Services.AddScoped<IChatService, ChatService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();
app.UseHttpsRedirection();

app.MapHub<ChatHub>("/chat");
app.MapControllers();

app.Run();

namespace LightsOn.WebApi
{
    public partial class Program { }
}