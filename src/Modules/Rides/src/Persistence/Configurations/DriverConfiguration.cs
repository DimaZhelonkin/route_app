using Ark.IdentityServer.Domain.ApplicationUser;
using Ark.Rides.Domain.Aggregates.Driver;
using Ark.Rides.Domain.Aggregates.RideParticipant;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ark.Rides.Persistence.Configurations;

public class DriverConfiguration : IEntityTypeConfiguration<Driver>
{
    #region IEntityTypeConfiguration<Driver> Members

    public void Configure(EntityTypeBuilder<Driver> builder)
    {
        builder.ToTable("Drivers");
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.IdentityId);
        builder.Property(x => x.IsLicensed)
               .IsRequired();

        // builder.Metadata.FindNavigation(nameof(Driver.OwnedVehicles))
        //     ?.SetPropertyAccessMode(PropertyAccessMode.Field);

        // builder.Navigation(x => x.OwnedVehicles)
        // .UsePropertyAccessMode(PropertyAccessMode.Field);
    }

    #endregion
}