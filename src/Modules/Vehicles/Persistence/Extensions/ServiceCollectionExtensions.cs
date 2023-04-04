using Ark.SharedLib.Persistence.Abstractions;
using Ark.SharedLib.Persistence.Extensions;
using Ark.Vehicles.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Ark.Vehicles.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext(configuration);
        services.AddScoped<IDbContextInitializer, DbContextInitializer>();

        services.AddScoped<IVehiclesRepository, VehiclesRepository>();
        services.AddScoped<IVehicleOwnersRepository, VehicleOwnersRepository>();
        return services;
    }

    private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("VehiclesConnection")!;
        services.AddCustomDbContext<VehiclesDbContext>(connectionString,
            o => o.UseNetTopologySuite());
        return services;
    }
}