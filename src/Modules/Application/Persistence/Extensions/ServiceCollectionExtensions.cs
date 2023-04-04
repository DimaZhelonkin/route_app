using Ark.Application.EventStore;
using Ark.Application.Repositories;
using Ark.SharedLib.Application.Abstractions.EventStore;
using Ark.SharedLib.Application.Abstractions.Repositories;
using Ark.SharedLib.Persistence.Abstractions;
using Ark.SharedLib.Persistence.Extensions;
using Ark.SharedLib.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Ark.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDatabasePersistence(configuration);
        services.AddEventStore();
        services.AddRepositories();
        return services;
    }

    private static IServiceCollection AddDatabasePersistence(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddApplicationDbContext(configuration);
        services.AddEventStoreDbContext(configuration);

        services.AddScoped<IDbContextInitializer, ApplicationDbContextInitializer>();
        services.AddScoped<IDbContextInitializer, EventStoreDbContextInitializer>();
        return services;
    }

    private static IServiceCollection AddApplicationDbContext(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ApplicationConnection")!;
        services.AddCustomDbContext<ApplicationDbContext>(connectionString);
        return services;
    }

    private static IServiceCollection AddEventStoreDbContext(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("EventStoreConnection")!;
        services.AddCustomDbContext<EventStoreDbContext>(connectionString);
        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IESRepository<,>), typeof(ESRepository<,>));
        services.AddScoped<IApplicationConfigurationRepository, ApplicationConfigurationRepository>();
        return services;
    }

    private static IServiceCollection AddEventStore(this IServiceCollection services)
    {
        services.AddScoped<IEventStore, EFEventStore>();
        services.AddScoped<IEventStoreSnapshotProvider, EFEventStoreSnapshotProvider>();
        services.AddScoped<IRetroactiveEventsService, RetroactiveEventsService>();
        return services;
    }
}