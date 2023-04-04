using System.Text.Json;
using Ark.SharedLib.Persistence.Audit;
using Ark.SharedLib.Persistence.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Ark.SharedLib.Persistence.Extensions;

public static class DbContextExtensions
{
    /// <summary>
    ///     Contains additional functionality when SaveChanges happens
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    public static async Task CustomSaveChangesAsync(this DbContext context,
        CancellationToken cancellationToken = default)
    {
        // var authenticatedUser = context.GetService<ICurrentUserService>();
        // DbContextStateOperations.Change(context.ChangeTracker, authenticatedUser.UserName);

        // context.EnsureAuditHistory(authenticatedUser.UserName);
        // var mediatr = context.GetService<IMediator>();
        // await mediatr.DispatchDomainEventsAsync<Guid>(context.ChangeTracker, cancellationToken);
    }

    /// <summary>
    ///     Ensures the automatic auditing history.
    /// </summary>
    /// <param name="dbContext">The context.</param>
    /// <param name="username">User made the action.</param>
    public static void EnsureAuditHistory(this DbContext dbContext, string? username)
    {
        var entries = dbContext.ChangeTracker.Entries()
                               .Where(e => !AuditUtilities.IsAuditDisabled(e.Entity.GetType()) &&
                                           e.State is EntityState.Added or EntityState.Modified or EntityState.Deleted)
                               .ToArray();
        foreach (var entry in entries) dbContext.Add((object)entry.AutoHistory(username));
    }

    private static AuditHistory AutoHistory(this EntityEntry entry, string? username)
    {
        var history = new AuditHistory
        {
            TableName = entry.Metadata.GetTableName()!,
            Username = username,
        };

        // Get the mapped properties for the entity type.
        // (include shadow properties, not include navigations & references)
        var properties = entry.Properties
                              .Where(p => !AuditUtilities.IsAuditDisabled(p.EntityEntry.Entity.GetType(),
                                  p.Metadata.Name));

        foreach (var prop in properties)
        {
            var propertyName = prop.Metadata.Name;
            if (prop.Metadata.IsPrimaryKey())
            {
                history.AutoHistoryDetails.NewValues[propertyName] = prop.CurrentValue;
                continue;
            }

            switch (entry.State)
            {
                case EntityState.Added:
                    history.RowId = "0";
                    history.Kind = EntityState.Added;
                    history.AutoHistoryDetails.NewValues.Add(propertyName, prop.CurrentValue);
                    break;

                case EntityState.Modified:
                    history.RowId = entry.PrimaryKey();
                    history.Kind = EntityState.Modified;
                    history.AutoHistoryDetails.OldValues.Add(propertyName, prop.OriginalValue);
                    history.AutoHistoryDetails.NewValues.Add(propertyName, prop.CurrentValue);
                    break;

                case EntityState.Deleted:
                    history.RowId = entry.PrimaryKey();
                    history.Kind = EntityState.Deleted;
                    history.AutoHistoryDetails.OldValues.Add(propertyName, prop.OriginalValue);
                    break;
            }
        }

        history.Changed = JsonSerializer.Serialize(history.AutoHistoryDetails);

        return history;
    }

    private static string PrimaryKey(this EntityEntry entry)
    {
        var key = entry.Metadata.FindPrimaryKey();

        var values = new List<object>();
        foreach (var property in key.Properties)
        {
            var value = entry.Property(property.Name).CurrentValue;
            if (value != null) values.Add(value);
        }

        return string.Join(",", values);
    }
}