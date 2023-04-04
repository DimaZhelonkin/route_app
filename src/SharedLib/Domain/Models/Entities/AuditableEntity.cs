using Ark.SharedLib.Domain.Interfaces;

namespace Ark.SharedLib.Domain.Models.Entities;

/// <inheritdoc cref="IAuditableEntity" />
/// <inheritdoc cref="RemovableEntity{TId}" />
public abstract class AuditableEntity<TId> : RemovableEntity<TId>, IAuditableEntity<TId>
{
    protected AuditableEntity(TId id) : base(id)
    {
    }

    #region IAuditableEntity<TId> Members

    public DateTimeOffset CreatedOnUtc { get; set; } = DateTimeOffset.UtcNow;

    public DateTimeOffset? ModifiedOnUtc { get; set; } = DateTimeOffset.UtcNow;

    public DateTimeOffset? DeletedOnUtc { get; set; }

    public string? CreatedBy { get; set; }
    public string? ModifiedBy { get; set; }

    #endregion
}