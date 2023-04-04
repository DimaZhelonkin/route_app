using Ark.Rides.Backoffice.Mappings;
using Microsoft.Extensions.DependencyInjection;

namespace Ark.Rides.Backoffice.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBackofficeServices(this IServiceCollection services)
    {
        var currentAssembly = typeof(RidesProfile).Assembly;

        services.AddMediatR(x =>
            x.RegisterServicesFromAssembly(currentAssembly)
        );
        services.AddAutoMapper(cfg =>
        {
            cfg.AddMaps(currentAssembly);
        });
        return services;
    }
}