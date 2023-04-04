using Ark.IdentityServer.DomainServices.Repositories;
using Ark.IdentityServer.Persistence.Repositories;
using Ark.SharedLib.Persistence.Abstractions;
using Ark.SharedLib.Persistence.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Ark.IdentityServer.Persistence.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDatabaseServices(configuration);
        return services;
    }

    private static IServiceCollection AddDatabaseServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext(configuration);
        services.AddScoped<IDbContextInitializer, DbContextInitializer>();

        services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
        return services;
    }

    private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("IdentityServerConnection")!;
        services.AddCustomDbContext<IdentityServerDbContext>(connectionString);
        return services;
    }
}