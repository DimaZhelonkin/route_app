using Ark.SharedLib.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Ark.SharedLib.Persistence.Extensions;

/// <summary>
/// </summary>
public static class ModelBuilderExtensions
{
    private static readonly ValueConverter<DateTime, DateTime> UtcValueConverter =
        new(outside => outside, inside => DateTime.SpecifyKind(inside, DateTimeKind.Utc));

    /// <summary>
    ///     Enables auditing change history.
    /// </summary>
    /// <param name="modelBuilder">The <see cref="ModelBuilder" /> to enable auto history functionality.</param>
    /// <returns>The <see cref="ModelBuilder" /> to enable auto history functionality.</returns>
    public static ModelBuilder EnableAuditHistory(this ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AuditHistoryEntityTypeConfiguration());

        return modelBuilder;
    }

    /// <summary>
    ///     Applies the UTC date-time converter to all of the properties that are <see cref="DateTime" /> and end with Utc.
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    static internal void ApplyUtcDateTimeConverter(this ModelBuilder modelBuilder)
    {
        foreach (var mutableEntityType in modelBuilder.Model.GetEntityTypes())
        {
            var dateTimeUtcProperties = mutableEntityType.GetProperties()
                                                         .Where(p => p.ClrType == typeof(DateTime) &&
                                                                     p.Name.EndsWith("Utc", StringComparison.Ordinal));

            foreach (var mutableProperty in dateTimeUtcProperties) mutableProperty.SetValueConverter(UtcValueConverter);
        }
    }
}