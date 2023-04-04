using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ark.Account.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
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
    }
}