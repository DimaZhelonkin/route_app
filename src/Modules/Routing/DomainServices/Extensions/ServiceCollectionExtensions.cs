using Microsoft.Extensions.DependencyInjection;

namespace Ark.Routing.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        return services;
    }
}