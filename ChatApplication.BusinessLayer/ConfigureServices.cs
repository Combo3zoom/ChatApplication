using System.Reflection;
using ChatApplication.Services.Chat.Commands.CreateChat;
using ChatApplication.Services.Chat.Commands.SendChatMessage;
using ChatApplication.Services.Chat.Queries.GetByIdChat;
using ChatApplication.Services.Chat.Queries.GetByUserIdChatsId;
using ChatApplication.Services.Common.Behaviors;
using ChatApplication.Services.Common.Exceptions;
using ChatApplication.Services.Message.Queries.GetByIdMessage;
using ChatApplication.Services.User.Queries.GetByIdUser;
using FluentValidation;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace ChatApplication.Services;

public static class ConfigureServices
{
    public static IServiceCollection AddBusinessLayerServices(this IServiceCollection services)
    {
        MapsterConfig.Configure();
        services.AddMapster();
        
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

public static class MapsterConfig
{
    public static void Configure()
    {
        TypeAdapterConfig<Database.Data.Models.Chat, SendChatMessageResponse>.NewConfig()
            .Map(dest => dest.Username, src => src.Name)
            .ConstructUsing(src => new SendChatMessageResponse(src.Name));;
        
        TypeAdapterConfig<Database.Data.Models.Chat, GetByIdChatQueryResponse>.NewConfig()
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.OwnerId, src => src.OwnerId)
            .ConstructUsing(src => new GetByIdChatQueryResponse(src.Id, src.Name, src.OwnerId));;
        
        TypeAdapterConfig<Database.Data.Models.Chat, GetChatsIdByUserIdQueryResponse>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .ConstructUsing(src => new GetChatsIdByUserIdQueryResponse(src.Id));
        
        TypeAdapterConfig<Database.Data.Models.Message, GetByIdMessageQueryResponse>.NewConfig()
            .Map(dest => dest.Text, src => src.Text)
            .Map(dest => dest.OwnerId, src => src.OwnerId)
            .ConstructUsing(src => new GetByIdMessageQueryResponse(src.Text, src.OwnerId));
        
        TypeAdapterConfig<Database.Data.Models.User, GetByIdUserQueryResponse>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .ConstructUsing(src => new GetByIdUserQueryResponse(src.Id, src.Name));
        
        TypeAdapterConfig<Database.Data.Models.User, SendChatMessageResponse>.NewConfig()
            .Map(dest => dest.Username, src => src.Name)
            .ConstructUsing(src => new SendChatMessageResponse(src.Name));
    }
}