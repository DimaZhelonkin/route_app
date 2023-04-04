using Microsoft.Extensions.DependencyInjection;

namespace Ark.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        return services;
    }
}