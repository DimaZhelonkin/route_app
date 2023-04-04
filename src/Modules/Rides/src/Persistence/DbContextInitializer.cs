using Ark.SharedLib.Persistence.Abstractions;
using Microsoft.Extensions.Logging;

namespace Ark.Rides.Persistence;

public class DbContextInitializer : BaseDbContextInitializer<RidesDbContext>
{
    private readonly RidesDbContext _dbContext;
    private readonly IServiceProvider _serviceProvider;

    public DbContextInitializer(RidesDbContext dbContext,
        ILogger<DbContextInitializer> logger, IServiceProvider serviceProvider)
        : base(dbContext, logger, serviceProvider)
    {
        _dbContext = dbContext;
        _serviceProvider = serviceProvider;
    }
}