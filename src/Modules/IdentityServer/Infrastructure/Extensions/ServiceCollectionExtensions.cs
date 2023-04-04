using System.Reflection;
using Ark.IdentityServer.Application.Contracts;
using Ark.IdentityServer.Application.Localisation;
using Ark.IdentityServer.Infrastructure.Keycloak.Mappings;
using Ark.IdentityServer.Infrastructure.Localisation;
using Ark.IdentityServer.Infrastructure.Services;
using Ark.IdentityServer.Infrastructure.UCaller.Mappings;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ark.IdentityServer.Infrastructure.Extensions;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var currentAssembly = Assembly.GetExecutingAssembly();
        services.AddMediatR(x =>
            x.RegisterServicesFromAssembly(currentAssembly)
        );
        services.AddAutoMapper(cfg =>
        {
            cfg.AddMaps(typeof(UCallerProfile).Assembly);
            cfg.AddMaps(typeof(KeyCloakProfile).Assembly);
        });

        services.AddCustomizedKeycloak(configuration);
        services.AddUCallerServices(configuration);

        services.AddScoped<IUserService, UserService>();
        services.AddTransient(typeof(INotificationHandler<>), typeof(ApplicationUserCreatedEventHandler<>));
        services.AddScoped<ILocalizationKeyProvider, LocalizationKeyProvider>();
        return services;
    }
}

public class ApplicationUserCreatedEventHandler<T>
{
}