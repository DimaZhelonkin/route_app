using Ark.SharedLib.Persistence.Abstractions;
using Ark.SharedLib.Persistence.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Ark.Account.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var currentAssembly = typeof(AccountDbContext).Assembly;

        services.AddAutoMapper(cfg =>
        {
            cfg.AddMaps(currentAssembly);
        });
        services.AddDbContext(configuration);
        services.AddScoped<IDbContextInitializer, DbContextInitializer>();
        return services;
    }

    private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("AccountConnection")!;
        services.AddCustomDbContext<AccountDbContext>(connectionString);
        return services;
    }
}