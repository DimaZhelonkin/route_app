using Ark.Rides.Domain.Aggregates.RideRequest;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ark.Rides.Persistence.Configurations;

public class RideRequestConfiguration : IEntityTypeConfiguration<RideRequest>
{
    #region IEntityTypeConfiguration<RideRequest> Members

    public void Configure(EntityTypeBuilder<RideRequest> builder)
    {
        builder.ToTable("RideRequests");
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.DriverRide)
               .WithMany();
        builder.HasOne(x => x.PassengerRide)
               .WithMany();
        builder.Property(x => x.Status)
               .UsePropertyAccessMode(PropertyAccessMode.Field);
    }

    #endregion
}