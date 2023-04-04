using Microsoft.Extensions.DependencyInjection;

namespace Ark.Vehicles.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBackofficeServices(this IServiceCollection services)
    {
        // var currentAssembly = typeof().Assembly;
        //
        // services.AddMediatR(x =>
        // x.RegisterServicesFromAssembly(currentAssembly)
        // );
        // services.AddAutoMapper(cfg =>
        // {
        //     cfg.AddMaps(currentAssembly);
        // });
        return services;
        return services;
    }
}