using System.Reflection;
using ChatApplication.Services.Chat.Commands.CreateChat;
using ChatApplication.Services.Common.Behaviors;
using ChatApplication.Services.Common.Exceptions;
using FluentValidation;
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
            cfg.AddOpenBehavior(typeof(RequestResponseLoggingBehavior<,>));
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
        
        return services;
    }
}