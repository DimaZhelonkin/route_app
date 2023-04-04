using Ark.Rides.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ark.Rides.Persistence.Configurations;

public class UserConfiguration<TId> : IEntityTypeConfiguration<User<TId>>
{
    #region IEntityTypeConfiguration<User<TId>> Members

    public void Configure(EntityTypeBuilder<User<TId>> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.IdentityId);
    }

    #endregion
}