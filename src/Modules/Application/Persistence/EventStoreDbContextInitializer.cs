using Ark.SharedLib.Persistence.Abstractions;
using Microsoft.Extensions.Logging;

namespace Ark.Application;

public class EventStoreDbContextInitializer : BaseDbContextInitializer<EventStoreDbContext>
{
    public EventStoreDbContextInitializer(EventStoreDbContext dbContext,
        ILogger<EventStoreDbContextInitializer> logger,
        IServiceProvider serviceProvider)
        : base(dbContext, logger, serviceProvider)
    {
    }
}