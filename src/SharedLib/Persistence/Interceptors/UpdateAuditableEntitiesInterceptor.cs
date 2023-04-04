using Ark.IdentityServer.Application.Contracts;
using Ark.SharedLib.Application.Identity;
using Ark.SharedLib.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Ark.SharedLib.Persistence.Interceptors;

public sealed class UpdateAuditableEntitiesInterceptor : SaveChangesInterceptor
{
    private readonly ICurrentUserService _currentUserService;

    public UpdateAuditableEntitiesInterceptor(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var dbContext = eventData.Context;
        if (dbContext is null)
            return await base.SavingChangesAsync(eventData, result, cancellationToken);

        var now = DateTimeOffset.UtcNow;
        var userId = _currentUserService.UserId;
        foreach (var change in dbContext.ChangeTracker.Entries())
        {
            if (change.Entity is IAuditableEntity auditableEntity)
            {
                if (change.State == EntityState.Added)
                {
                    auditableEntity.CreatedOnUtc = now;
                    auditableEntity.CreatedBy = userId;
                }

                auditableEntity.ModifiedOnUtc = now;
                auditableEntity.ModifiedBy = userId;

                if (change is {Entity: ISoftDeletableEntity, State: EntityState.Deleted})
                    auditableEntity.DeletedOnUtc = now;
            }

            if (change is {Entity: ISoftDeletableEntity safetyRemovableEntity, State: EntityState.Deleted})
            {
                safetyRemovableEntity.MarkAsDeleted();
                change.State = EntityState.Modified;
            }
        }

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}