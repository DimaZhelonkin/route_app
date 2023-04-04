using Ark.Account.Controllers;
using Ark.Account.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace Ark.Account.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBackofficeServices(this IServiceCollection services)
    {
        var currentAssembly = typeof(ProfileController).Assembly;
        services.AddAutoMapper(config =>
        {
            config.AddMaps(typeof(AccountProfile));
        });
        services.AddMediatR(x =>
            x.RegisterServicesFromAssembly(currentAssembly)
        );
        
        return services;
    }
}