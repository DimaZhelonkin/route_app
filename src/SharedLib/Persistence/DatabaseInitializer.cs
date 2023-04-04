using System.Data.Common;
using Ark.SharedLib.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using ILogger2 = Serilog.ILogger;

namespace Ark.SharedLib.Persistence;

public static class DatabaseInitializer
{
    public static async Task DatabaseInitialisingAsync(string[] args, IServiceProvider serviceProvider)
    {
        var dbInitializers = serviceProvider.GetServices<IDbContextInitializer>();
        foreach (var dbInitializer in dbInitializers)
        {
            var logger = serviceProvider.GetRequiredService<ILogger2>();
            try
            {
                var needsInit = args.Contains("/init");
                var needsMigrate = needsInit || args.Contains("/migrate");
                var needsSeed = needsInit || args.Contains("/seed");
                if (needsInit || needsMigrate || needsSeed)
                    await dbInitializer.InitAsync(needsMigrate, needsSeed);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "An error occurred while running database migration");
            }
        }
    }

    public static void MigrateDbContext<TContext>(IServiceProvider serviceProvider,
        Action<TContext, IServiceProvider> seeder, bool underK8s = false)
        where TContext : DbContext
    {
        using var scope = serviceProvider.CreateScope();
        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILogger<TContext>>();
        var context = services.GetRequiredService<TContext>();

        try
        {
            logger.LogInformation("Migrating database associated with context {DbContextName}", typeof(TContext).Name);

            if (underK8s)
                InvokeSeeder(seeder, context, services);
            else
            {
                var retries = 10;
                var retry = Policy.Handle<DbException>()
                                  .WaitAndRetry(
                                      retries,
                                      retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                                      (exception, timeSpan, retry, ctx) =>
                                      {
                                          logger.LogWarning(exception,
                                              "[{Prefix}] Exception {ExceptionType} with message {Message} detected on attempt {Retry} of {Retries}",
                                              nameof(TContext), exception.GetType().Name, exception.Message, retry,
                                              retries);
                                      });

                //if the sql server container is not created on run docker compose this
                //migration can't fail for network related exception. The retry options for DbContext only 
                //apply to transient exceptions
                // Note that this is NOT applied when running some orchestrators (let the orchestrator to recreate the failing service)
                retry.Execute(() => InvokeSeeder(seeder, context, services));
            }

            logger.LogInformation("Migrated database associated with context {DbContextName}", typeof(TContext).Name);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while migrating the database used on context {DbContextName}",
                typeof(TContext).Name);
            if (underK8s) throw; // Rethrow under k8s because we rely on k8s to re-run the pod
        }
    }

    private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder, TContext context,
        IServiceProvider services)
        where TContext : DbContext
    {
        context.Database.Migrate();
        seeder(context, services);
    }
}