using Ark.Application.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ark.Application.Configurations;

public class ApplicationConfigurationEntityTypeConfiguration : IEntityTypeConfiguration<ApplicationConfiguration>
{
    #region IEntityTypeConfiguration<ApplicationConfiguration> Members

    public void Configure(EntityTypeBuilder<ApplicationConfiguration> builder)
    {
        builder.Property(e => e.Id).HasMaxLength(256);
        builder.Property(e => e.Value).IsRequired().HasMaxLength(512);
        builder.Property(e => e.Description).HasMaxLength(1024);
        builder.ToTable("ApplicationConfigurations");
    }

    #endregion
}