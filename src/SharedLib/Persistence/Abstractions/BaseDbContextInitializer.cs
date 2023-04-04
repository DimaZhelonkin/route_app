using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ark.SharedLib.Persistence.Abstractions;

public abstract class BaseDbContextInitializer<TDbContext> : IDbContextInitializer where TDbContext : DbContext
{
    protected readonly ILogger<BaseDbContextInitializer<TDbContext>> Logger;
    private readonly IServiceProvider _serviceProvider;
    protected readonly TDbContext DbContext;

    protected BaseDbContextInitializer(
        TDbContext dbContext,
        ILogger<BaseDbContextInitializer<TDbContext>> logger,
        IServiceProvider serviceProvider)
    {
        DbContext = dbContext;
        Logger = logger;
        _serviceProvider = serviceProvider;
    }

    #region IDbContextInitializer Members

    public async Task InitAsync(bool migrate = true, bool seed = true, CancellationToken cancellationToken = default)
    {
        try
        {
            if (!migrate && !seed) return;

            Logger.LogInformation("Database initialization started");
            if (migrate)
            {
                Logger.LogInformation("Database migrating started");
                await MigrateAsync(cancellationToken);
                Logger.LogInformation("Database migrating finished");
            }

            if (seed)
            {
                Logger.LogInformation("Database seeding started");
                await SeedAsync(cancellationToken);
                Logger.LogInformation("Database seeding finished");
            }

            Logger.LogInformation("Database initialization finished");
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "An error occurred while running database migration");
        }
    }

    public Task MigrateAsync(CancellationToken cancellationToken = default)
    {
        DatabaseInitializer.MigrateDbContext<TDbContext>(_serviceProvider, (context, provider) => { });
        return Task.CompletedTask;
    }

    public virtual Task SeedAsync(CancellationToken cancellationToken = default) =>
        Task.CompletedTask;

    #endregion
}