using Ark.SharedLib.Persistence.Abstractions;
using Microsoft.Extensions.Logging;

namespace Ark.Vehicles;

public class DbContextInitializer : BaseDbContextInitializer<VehiclesDbContext>
{
    public DbContextInitializer(VehiclesDbContext dbContext,
        ILogger<DbContextInitializer> logger,
        IServiceProvider serviceProvider)
        : base(dbContext, logger, serviceProvider)
    {
    }
}