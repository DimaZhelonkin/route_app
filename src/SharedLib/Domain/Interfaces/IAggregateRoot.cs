namespace Ark.SharedLib.Domain.Interfaces;

/// <summary>
///     Interface for aggregate root
/// </summary>
public interface IAggregateRoot<out TId> : IEntity<TId>, IAggregateRoot
{
}

public interface IAggregateRoot
{
    /// <summary>
    ///     The aggregate root current version
    /// </summary>
    long Version { get; }

    void ApplyEvent(IDomainEvent @event, long version);
    IEnumerable<IDomainEvent> GetDomainEvents();
    void ClearDomainEvents();
    public void AddEvent(IDomainEvent @event);
    public void RemoveEvent(IDomainEvent @event);
}