using System.Reflection;
using ChatApplication.Services.Chat.Commands.CreateChat;
using ChatApplication.Services.Chat.Commands.DeleteChat;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ChatApplication.Services;

public static class ConfigureServices
{
    public static IServiceCollection AddBusinessLayerServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(typeof(CreateChatCommandHandler).Assembly);
        });
        
        return services;
    }
}