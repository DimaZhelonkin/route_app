using Ark.Rides.Application.Contracts;
using Ark.Rides.Application.Features.Driver.Commands.CreateDriverRide;
using Ark.Rides.Application.Providers;
using Ark.Rides.Application.Services;
using Ark.Rides.DomainServices.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Ark.Rides.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddDomainServices();
        var currentAssembly = typeof(CreateDriverRideCommand).Assembly;
        services.AddHttpContextAccessor();

        services.AddMediatR(x =>
            x.RegisterServicesFromAssembly(currentAssembly)
        );
        services.AddAutoMapper(cfg =>
        {
            cfg.AddMaps(currentAssembly);
        });

        services.AddTransient<IRidesService, RidesService>();
        services.AddTransient<IDriverVehiclesService, DriverVehiclesService>();
        services.AddScoped<ICurrentDriverService, CurrentDriverService>();
        services.AddScoped<ICurrentPassengerService, CurrentPassengerService>();
        services.AddScoped<IDriversProvider, DriversProvider>();
        services.AddScoped<IPassengersProvider, PassengersProvider>();
        services.AddScoped<IDriverRidesProvider, DriverRidesProvider>();
        return services;
    }
}