using Ark.SharedLib.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Ark.SharedLib.Persistence.Utilities;

/// <summary>
///     Contains a set of automatic operations against Database
/// </summary>
public static class DbContextStateOperations
{
    public static void Change(ChangeTracker changeTracker, string? user)
    {
        var now = DateTimeOffset.UtcNow;

        foreach (var change in changeTracker.Entries())
        {
            if (change.Entity is IAuditableEntity auditableEntity)
            {
                if (change.State == EntityState.Added)
                {
                    auditableEntity.CreatedOnUtc = now;
                    auditableEntity.CreatedBy = user;
                }

                auditableEntity.ModifiedOnUtc = now;
                auditableEntity.ModifiedBy = user;

                if (change is {Entity: ISoftDeletableEntity, State: EntityState.Deleted})
                    auditableEntity.DeletedOnUtc = now;
            }

            if (change is {Entity: ISoftDeletableEntity safetyRemovableEntity, State: EntityState.Deleted})
            {
                safetyRemovableEntity.MarkAsDeleted();
                change.State = EntityState.Modified;
            }
        }
    }
}