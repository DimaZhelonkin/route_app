using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Ark.Routing.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBackofficeServices(this IServiceCollection services)
    {
        var currentAssembly = Assembly.GetExecutingAssembly();

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