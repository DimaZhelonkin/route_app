using Ark.Rides.Domain.Aggregates.DriverRide;
using Ark.Vehicles.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetTopologySuite.Geometries;

namespace Ark.Rides.Persistence.Configurations;

public class DriverRideConfiguration : IEntityTypeConfiguration<DriverRide>
{
    #region IEntityTypeConfiguration<DriverRide> Members

    public void Configure(EntityTypeBuilder<DriverRide> builder)
    {
        builder.ToTable("DriverRides");
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.VehicleId);
        builder.HasIndex(x => x.RouteId);
        builder.Property(x => x.FactRoute)
               .HasColumnType($"Geometry ({Geometry.TypeNameLineString})");
        builder.Navigation(x => x.PassengersRides)
               .UsePropertyAccessMode(PropertyAccessMode.Field);
    }

    #endregion
}