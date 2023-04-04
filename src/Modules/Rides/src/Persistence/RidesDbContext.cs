using Ark.Rides.Domain.Aggregates.Driver;
using Ark.Rides.Domain.Aggregates.DriverRide;
using Ark.Rides.Domain.Aggregates.Passenger;
using Ark.Rides.Domain.Aggregates.PassengerRide;
using Ark.Rides.Domain.Aggregates.Ride;
using Ark.Rides.Domain.Aggregates.RideRequest;
using Ark.SharedLib.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Ark.Rides.Persistence;

public class RidesDbContext : BaseDbContext
{
    private const string PostGisExtensionName = "postgis";

    public RidesDbContext(DbContextOptions<RidesDbContext> options) : base(options)
    {
    }

    public DbSet<Passenger> Passengers { get; set; }
    public DbSet<Driver> Drivers { get; set; }
    public DbSet<DriverRide> DriverRides { get; set; }
    public DbSet<PassengerRide> PassengerRides { get; set; }
    public DbSet<RideRequest> RideRequests { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasPostgresExtension(PostGisExtensionName);
      
        // builder.EnableDetailedErrors();
        // builder.UseLazyLoadingProxies();  
    }
}