using Ardalis.GuardClauses;
using Ark.Application.Entities;
using Ark.SharedLib.Application.Abstractions.EventStore;
using Ark.SharedLib.Application.Exceptions;
using Ark.SharedLib.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Ark.Application.EventStore;

/// <summary>
///     An implementation of <see cref="IEventStore" /> using Entity Framework
/// </summary>
/// <inheritdoc cref="IEventStore" />
internal class EFEventStore : IEventStore
{
    #region Public Constructors

    public EFEventStore(EventStoreDbContext context)
    {
        _context = context;
        _events = _context.Set<Event>();
    }

    #endregion Public Constructors

    private Event ConstructEventEntity<TAggregateId>(IDomainEvent<TAggregateId> domainEvent, long expectedVersion,
        string aggregateName)
    {
        if (domainEvent.AggregateVersion > expectedVersion)
            throw new EventStoreException(
                $"Concurrency issue detected when saving events. Event found with version {domainEvent.AggregateVersion} which is larger than maximum expected version {expectedVersion}");
        var domainEventType = domainEvent.GetType();
        return new Event(domainEvent.EventId)
        {
            AggregateId = domainEvent.AggregateId.ToString(),
            AggregateName = aggregateName,
            Name = domainEventType.Name,
            AssemblyTypeName = domainEventType.AssemblyQualifiedName,
            Data = JsonConvert.SerializeObject(domainEvent),
            Version = domainEvent.AggregateVersion,
            CreatedAt = DateTimeOffset.UtcNow, //duplicate. save or lose??
        };
    }

    #region Private Fields

    private readonly EventStoreDbContext _context;
    private readonly DbSet<Event> _events;

    #endregion Private Fields

    #region Public Methods

    /// <inheritdoc cref="IEventStore.LoadAsync{TAggregateId}(string, string, int, int)" />
    /// <exception cref="ArgumentException"></exception>
    public async Task<IReadOnlyCollection<IDomainEvent<TAggregateId>>> LoadAsync<TAggregateId>(string aggregateRootId,
        string aggregateName, long fromVersion, long toVersion)
    {
        Guard.Against.Negative(fromVersion, nameof(fromVersion));
        Guard.Against.Negative(toVersion, nameof(toVersion));
        if (fromVersion > toVersion)
            throw new ArgumentException($"{nameof(fromVersion)} cannot be grated than {nameof(toVersion)}");
        IQueryable<Event> events = _events
                                   .Where(e => e.AggregateId == aggregateRootId && e.AggregateName == aggregateName &&
                                               e.Version >= fromVersion && e.Version <= toVersion)
                                   .OrderBy(de => de.Version);
        var domainEvents = new List<IDomainEvent<TAggregateId>>();
        //get events
        foreach (var @event in events)
        {
            var domainEvent =
                DomainEventHelper.ConstructDomainEvent<TAggregateId>(@event.Data, @event.AssemblyTypeName);
            domainEvents.Add(domainEvent);
        }

        return domainEvents.AsReadOnly();
    }

    public async Task SaveAsync<TAggregateId>(string aggregateName, long expectedVersion,
        IEnumerable<IDomainEvent<TAggregateId>> domainEvents)
    {
        foreach (var domainEvent in domainEvents)
        {
            var eventEntity = ConstructEventEntity(domainEvent, expectedVersion, aggregateName);
            await _events.AddAsync(eventEntity);
        }

        await _context.SaveChangesAsync();
    }

    public async Task SaveAsync<TAggregateId>(string aggregateName, long expectedVersion,
        IDomainEvent<TAggregateId> domainEvent)
    {
        var eventEntity = ConstructEventEntity(domainEvent, expectedVersion, aggregateName);
        await _events.AddAsync(eventEntity);
        await _context.SaveChangesAsync();
    }

    #endregion Public Methods
}