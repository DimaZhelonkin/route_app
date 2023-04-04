using MediatR;

namespace Ark.SharedLib.Domain.Interfaces;

/// <summary>
///     Interface for domain events
/// </summary>
public interface IDomainEvent : INotification
{
    /// <summary>
    ///     The event identifier
    /// </summary>
    Guid EventId { get; }
}

/// <summary>
///     Generic interface for domain events with aggregate id
/// </summary>
public interface IDomainEvent<TAggregateId> : IDomainEvent
{
    /// <summary>
    ///     The identifier of the aggregate which has generated the event
    /// </summary>
    TAggregateId AggregateId { get; protected internal set; } // TODO временный костыль

    /// <summary>
    ///     The version of the aggregate when the event has been generated
    /// </summary>
    long AggregateVersion { get; }
}