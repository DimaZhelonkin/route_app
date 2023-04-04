using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Ark.IdentityServer.DomainServices.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        var currentAssembly = Assembly.GetExecutingAssembly();
        services.AddMediatR(x =>
            x.RegisterServicesFromAssembly(currentAssembly)
        );
        return services;
    }
}