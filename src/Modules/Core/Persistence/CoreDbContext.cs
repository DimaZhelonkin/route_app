using Ark.SharedLib.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Ark.Core;

public class CoreDbContext : BaseDbContext
{
    private const string PostGisExtensionName = "postgis";

    public CoreDbContext(DbContextOptions<CoreDbContext> options) : base(options)
    {
    }

    // public DbSet<Route> Routes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder) => base.OnModelCreating(builder);
    // builder.HasPostgresExtension(PostGisExtensionName);
    // builder.EnableDetailedErrors();
    // builder.UseLazyLoadingProxies();  
}