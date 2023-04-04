using System.Reflection;
using Ark.SharedLib.Application.Behaviours;
using Ark.SharedLib.Application.Identity;
using Ark.SharedLib.Common.Results.Mapping;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Ark.SharedLib.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var currentAssembly = Assembly.GetExecutingAssembly();

        services.AddAutoMapper(cfg =>
        {
            cfg.AddMaps(currentAssembly, typeof(ResultProfile).Assembly);
            // cfg.AddProfile<SpecificationProfile>();
            // cfg.AddProfile<OrderProfile>();
        });
        services.AddTransient(typeof(ResultToObjectTypeConverter<>));

        services.AddValidatorsFromAssembly(currentAssembly);
        services.AddMediatR(x =>
            x.RegisterServicesFromAssembly(currentAssembly)
        );

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionPipelineBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehaviour<,>));
        // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionPipeline<,>));
       
        // services.AddTransient(typeof(SpecificationTypeConverter<,>));
        // services.AddTransient(typeof(SpecificationInterfaceTypeConverter<,>));
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IIdentityService, IdentityService>();
        return services;
    }
}