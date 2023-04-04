using Ark.Routing.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetTopologySuite.Geometries;

namespace Ark.Routing.Configurations;

public class RouteEntityTypeConfiguration : IEntityTypeConfiguration<Route>
{
    #region IEntityTypeConfiguration<Route> Members

    public void Configure(EntityTypeBuilder<Route> builder)
    {
        // builder.Property(x=>x.Points).HasColumnType(Geometry.TypeNameLineString);
        builder.Property(x => x.Current).HasColumnType($"Geometry ({Geometry.TypeNameLineString})");
        // builder.Property(x=>x.Location).HasColumnType("geometry (point)");
        builder.ToTable("Routes");
    }

    #endregion
}