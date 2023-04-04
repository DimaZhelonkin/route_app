using Ark.SharedLib.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ark.IdentityServer.Persistence;

public class DbContextInitializer : BaseDbContextInitializer<IdentityServerDbContext>
{
    public DbContextInitializer(IdentityServerDbContext dbContext,
        ILogger<DbContextInitializer> logger,
        IServiceProvider serviceProvider)
        : base(dbContext, logger, serviceProvider)
    {
    }
}