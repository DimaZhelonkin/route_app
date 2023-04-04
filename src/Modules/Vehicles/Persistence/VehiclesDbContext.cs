using Ark.SharedLib.Persistence.DbContexts;
using Ark.Vehicles.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Ark.Vehicles;

public class VehiclesDbContext : BaseDbContext
{
    private const string PostGisExtensionName = "postgis";

    public VehiclesDbContext(DbContextOptions<VehiclesDbContext> options) : base(options)
    {
    }

    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<VehicleOwner> VehicleOwners { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasPostgresExtension(PostGisExtensionName);
        // builder.EnableDetailedErrors();
        // builder.UseLazyLoadingProxies();  
    }
}