namespace Ark.SharedLib.Domain.Interfaces;

/// <summary>
///     Interface indicates that the record in database is not deleting, but the entity marks as deleted
/// </summary>
public interface ISoftDeletableEntity
{
    /// <summary>
    ///     Gets a value indicating whether the entity has been deleted.
    /// </summary>
    bool IsDeleted { get; }

    /// <summary>
    ///     Gets the date and time in UTC format the entity was deleted on.
    /// </summary>
    DateTimeOffset? DeletedOnUtc { get; set; }

    void MarkAsDeleted();
}