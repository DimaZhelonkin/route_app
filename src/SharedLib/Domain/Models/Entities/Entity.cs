using Ark.SharedLib.Domain.Interfaces;

namespace Ark.SharedLib.Domain.Models.Entities;

public abstract class Entity<TId> : IEntity<TId>
{
    protected Entity(TId id)
    {
        Id = id;
    }

    #region IEntity<TId> Members

    /// <summary>
    ///     The aggregate root Id
    /// </summary>
    public TId Id { get; init; }

    #endregion
}