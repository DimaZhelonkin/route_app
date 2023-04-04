using Ark.Vehicles.Contracts;
using Ark.Vehicles.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Ark.Vehicles.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services) =>
        // var currentAssembly = typeof(IVehiclesProviderService).Assembly;
        // services.AddMediatR(x =>
        // x.RegisterServicesFromAssembly(currentAssembly)
        // );
        // services.AddAutoMapper(cfg =>
        // {
        //     cfg.AddMaps(currentAssembly);
        // });
        services.AddTransient<IVehiclesProviderService, VehiclesProviderService>();
}