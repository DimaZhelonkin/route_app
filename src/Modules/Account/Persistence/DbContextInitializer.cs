using Ark.SharedLib.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ark.Account;

public class DbContextInitializer : BaseDbContextInitializer<AccountDbContext>
{

    public DbContextInitializer(AccountDbContext dbContext, ILogger<DbContextInitializer> logger,
        IServiceProvider serviceProvider)
        : base(dbContext,logger, serviceProvider)
    {
    }

    public override Task SeedAsync(CancellationToken cancellationToken = default) =>
        Task.CompletedTask;
}