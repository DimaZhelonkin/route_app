using Ark.Account.Mappings;
using Microsoft.Extensions.DependencyInjection;

namespace Ark.Account.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var currentAssembly = typeof(AccountProfile).Assembly;
        services.AddMediatR(x =>
            x.RegisterServicesFromAssembly(currentAssembly)
        );
        services.AddAutoMapper(x => x.AddMaps(currentAssembly));
        return services;
    }
}