using Ark.Rides.Domain.Aggregates.PassengerRide;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetTopologySuite.Geometries;

namespace Ark.Rides.Persistence.Configurations;

public class PassengerRideConfiguration : IEntityTypeConfiguration<PassengerRide>
{
    #region IEntityTypeConfiguration<PassengerRide> Members

    public void Configure(EntityTypeBuilder<PassengerRide> builder)
    {
        builder.ToTable("PassengerRides");
        builder.HasKey(x => x.Id);
        // builder.Property(x => x.Id)
               // .HasConversion(x => x.Value, v => new PassengerRideId(v));
        builder.HasIndex(x => x.RouteId);
        builder.Property(x => x.FactRoute)
               .HasColumnType($"Geometry ({Geometry.TypeNameLineString})");
        builder.Navigation(x => x.DriversRides)
               .UsePropertyAccessMode(PropertyAccessMode.Field);
    }

    #endregion
}