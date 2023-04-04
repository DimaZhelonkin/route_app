using Ark.Rides.Application.Features.Driver.Commands.CreateDriverRide;
using Ark.Rides.Application.Repositories;
using Ark.Rides.Persistence.Repositories;
using Ark.SharedLib.Application.Abstractions.Repositories;
using Ark.SharedLib.Persistence.Abstractions;
using Ark.SharedLib.Persistence.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Ark.Rides.Persistence.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext(configuration);
        services.AddScoped<IDbContextInitializer, DbContextInitializer>();

        services.AddScoped<IDriverRidesRepository, DriverRidesRepository>();
        services.AddScoped<IPassengerRidesRepository, PassengerRidesRepository>();
        services.AddScoped<IRideRequestsRepository, RideRequestsRepository>();
        services.AddScoped<IDriversRepository, DriversRepository>();
        services.AddScoped<IPassengersRepository, PassengersRepository>();
        return services;
    }

    private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("RidesConnection")!;
        services.AddCustomDbContext<RidesDbContext>(connectionString, o => o.UseNetTopologySuite());
        // container?.RegisterConditional<IUnitOfWork, RidesDbContext>(Lifestyle.Scoped, InCurrentModule());
        return services;
    }

    // private static Predicate<PredicateContext> InCurrentModule() =>
    //     c =>
    //     {
    //         var assemblyName = c.Consumer.ImplementationType.Assembly.GetName().Name!;
    //         var assemblyNameParts = assemblyName.Split(".");
    //         if (assemblyNameParts.Length < 2) return false;
    //         var moduleName = assemblyNameParts[1];
    //         return assemblyName.Contains(moduleName);
    //     };
}