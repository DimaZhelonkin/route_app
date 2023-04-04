namespace Ark.SharedLib.Domain.Models.Entities;

public abstract class RemovableEntity<TId> : Entity<TId>
{
    protected RemovableEntity(TId id) : base(id)
    {
        Id = id;
    }

    /// <summary>
    ///     Indicates whether this aggregate is logically deleted
    /// </summary>
    public bool IsDeleted { get; private set; }

    public DateTimeOffset? DeletedOnUtc { get; set; }

    public void MarkAsDeleted()
    {
        IsDeleted = true;
        DeletedOnUtc = DateTimeOffset.UtcNow;
    }
}