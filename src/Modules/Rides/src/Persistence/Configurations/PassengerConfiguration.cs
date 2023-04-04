using Ark.Rides.Domain.Aggregates.Passenger;
using Ark.Rides.Domain.Aggregates.RideParticipant;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ark.Rides.Persistence.Configurations;

public class PassengerConfiguration : IEntityTypeConfiguration<Passenger>
{
    #region IEntityTypeConfiguration<Passenger> Members

    public void Configure(EntityTypeBuilder<Passenger> builder)
    {
        builder.ToTable("Passengers");
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.IdentityId);
    }

    #endregion
}