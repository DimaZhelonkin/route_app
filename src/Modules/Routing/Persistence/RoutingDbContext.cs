using Ark.Routing.Aggregates;
using Ark.SharedLib.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Ark.Routing;

public class RoutingDbContext : BaseDbContext
{
    private const string PostGisExtensionName = "postgis";

    public RoutingDbContext(DbContextOptions<RoutingDbContext> options) : base(options)
    {
    }

    public DbSet<Route> Routes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasPostgresExtension(PostGisExtensionName);
        // builder.EnableDetailedErrors();
        // builder.UseLazyLoadingProxies();  
    }
}