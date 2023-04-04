using Ark.Vehicles.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ark.Vehicles.Configurations;

public class VehicleOwnerConfiguration : IEntityTypeConfiguration<VehicleOwner>
{
    public void Configure(EntityTypeBuilder<VehicleOwner> builder)
    {
        builder.ToTable("VehicleOwners");
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.IdentityId);
    }
}