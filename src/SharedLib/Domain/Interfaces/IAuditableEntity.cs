namespace Ark.SharedLib.Domain.Interfaces;

/// <summary>
///     Base auditable entity class to be inherited from entities which should be audited
/// </summary>
public interface IAuditableEntity
{
    /// <summary>
    ///     The date the entity was created
    /// </summary>
    public DateTimeOffset CreatedOnUtc { get; set; }

    /// <summary>
    ///     The date the entity was last modified
    /// </summary>
    public DateTimeOffset? ModifiedOnUtc { get; set; }

    /// <summary>
    ///     The date and time in UTC format the entity was deleted on.
    /// </summary>
    DateTimeOffset? DeletedOnUtc { get; set; }

    /// <summary>
    ///     The user who created the entity
    /// </summary>
    public string? CreatedBy { get; set; }

    /// <summary>
    ///     The user who last modified the entity
    /// </summary>
    public string? ModifiedBy { get; set; }
}

public interface IAuditableEntity<out TId> : IAuditableEntity, IEntity<TId>
{
}