using Ark.Routing.Features.GetRouteQuery;
using Microsoft.Extensions.DependencyInjection;

namespace Ark.Routing.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var currentAssembly = typeof(GetRouteQuery).Assembly;

        services.AddMediatR(x =>
            x.RegisterServicesFromAssembly(currentAssembly)
        );
        // services.AddAutoMapper(cfg =>
        // {
        //     cfg.AddMaps(currentAssembly);
        // });
        return services;
    }
}