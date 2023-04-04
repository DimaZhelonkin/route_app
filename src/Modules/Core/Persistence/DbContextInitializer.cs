using Ark.SharedLib.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ark.Core;

public class DbContextInitializer : BaseDbContextInitializer<CoreDbContext>
{
    public DbContextInitializer(CoreDbContext dbContext,
        ILogger<DbContextInitializer> logger,
        IServiceProvider serviceProvider)
        : base(dbContext, logger, serviceProvider)
    {
    }
}