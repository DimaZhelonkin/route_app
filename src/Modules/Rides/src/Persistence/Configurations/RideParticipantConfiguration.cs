using Ark.IdentityServer.Domain.ApplicationUser;
using Ark.Rides.Domain.Aggregates;
using Ark.Rides.Domain.Aggregates.RideParticipant;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ark.Rides.Persistence.Configurations;

public class RideParticipantConfiguration<TId> : IEntityTypeConfiguration<RideParticipant<TId>>
    where TId : RideParticipantId
{
    #region IEntityTypeConfiguration<RideParticipant> Members

    public void Configure(EntityTypeBuilder<RideParticipant<TId>> builder)
    {
        builder.HasBaseType<User<TId>>();
    }

    #endregion
}