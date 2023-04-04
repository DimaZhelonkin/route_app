using Ark.Application.Aggregates;
using Ark.SharedLib.Persistence.Abstractions;
using Microsoft.Extensions.Logging;

namespace Ark.Application;

public class ApplicationDbContextInitializer : BaseDbContextInitializer<ApplicationDbContext>
{
    public ApplicationDbContextInitializer(ApplicationDbContext dbContext,
        ILogger<ApplicationDbContextInitializer> logger,
        IServiceProvider serviceProvider)
        : base(dbContext, logger, serviceProvider)
    {
    }

    private const string User = "DbInitializer";

    public override Task SeedAsync(CancellationToken cancellationToken = default)
    {
        Logger.LogInformation("ApplicationContext seeding is starting");
        var appConfigurations = DbContext.ApplicationConfigurations;
        if (!appConfigurations.Any())
        {
            appConfigurations.Add(new ApplicationConfiguration("50",
                "The number of events after which a snapshot in the event store will be taken")
            {
                Id = "EventStoreSnapshotFrequency",
                CreatedBy = User,
            });
            DbContext.SaveChanges();
        }

        Logger.LogInformation("ApplicationContext seeding finished");
        return Task.CompletedTask;
    }
}