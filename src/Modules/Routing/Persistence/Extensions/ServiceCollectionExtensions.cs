using Ark.Routing.Repositories;
using Ark.SharedLib.Persistence.Abstractions;
using Ark.SharedLib.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Ark.Routing.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext(configuration);
        services.AddScoped<IDbContextInitializer, DbContextInitializer>();

        services.AddScoped<IRouteRepository, RouteRepository>();
        return services;
    }

    private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("RoutingConnection")!;
        services.AddCustomDbContext<RoutingDbContext>(connectionString,
            o => o.UseNetTopologySuite());
        return services;
    }
}