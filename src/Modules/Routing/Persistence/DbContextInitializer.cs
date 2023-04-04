using Ark.SharedLib.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ark.Routing;


public class DbContextInitializer : BaseDbContextInitializer<RoutingDbContext>
{
    public DbContextInitializer(RoutingDbContext dbContext,
        ILogger<DbContextInitializer> logger,
        IServiceProvider serviceProvider)
        : base(dbContext, logger, serviceProvider)
    {
    }
}