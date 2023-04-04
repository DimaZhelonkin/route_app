using System.Reflection;
using Ark.SharedLib.Domain.Interfaces;
using Ark.SharedLib.Domain.Models.Events;

namespace Ark.SharedLib.Domain.Models.Entities;

// TODO Should inherits BaseEntity (AuditableEntity сделан для упрощения)
public abstract class AggregateRootBase<TId> : AuditableEntity<TId>, IAggregateRoot<TId>
{
    private const long NewAggregateVersion = -1;

    private readonly ICollection<IDomainEvent> _domainEvents = new LinkedList<IDomainEvent>();

    protected AggregateRootBase(TId id) : base(id)
    {
    }

    #region IAggregateRoot<TId> Members

    public long Version { get; private set; } = NewAggregateVersion;

    void IAggregateRoot.ApplyEvent(IDomainEvent @event, long version)
    {
        if (!_domainEvents.Any(x => Equals(x.EventId, @event.EventId)))
            try
            {
                InvokeHandler(@event);
                Version = version;
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }

        Version = version;
    }

    void IAggregateRoot.ClearDomainEvents() => _domainEvents.Clear();

    public void AddEvent(IDomainEvent @event) => _domainEvents.Add(@event);

    public void RemoveEvent(IDomainEvent @event) => _domainEvents.Remove(@event);

    IEnumerable<IDomainEvent> IAggregateRoot.GetDomainEvents() => _domainEvents.AsEnumerable();

    #endregion


    protected void RaiseEvent<TEvent>(TEvent @event)
        where TEvent : DomainEvent<TId>
    {
        var version = Version + 1;
        var eventWithAggregate = @event.WithAggregate(
            Equals(Id, default(TId)) ? @event.AggregateId : Id,
            version);

        ((IAggregateRoot<TId>)this).ApplyEvent(eventWithAggregate, version);
        _domainEvents.Add(eventWithAggregate);
    }

    private void InvokeHandler(IDomainEvent @event)
    {
        var handlerMethod = GetEventHandlerMethodInfo(@event);
        handlerMethod.Invoke(this, new object[] {@event});
    }

    private MethodInfo GetEventHandlerMethodInfo(IDomainEvent @event)
    {
        var handlerType = GetType()
                          .GetInterfaces()
                          .Where(t => t.IsGenericType)
                          .Single(i =>
                              i.GetGenericTypeDefinition() == typeof(IDomainEventHandler<>) &&
                              i.GetGenericArguments()[0] == @event.GetType());
        var eventHandler = handlerType.GetTypeInfo().GetDeclaredMethod(nameof(IDomainEventHandler<IDomainEvent>.Apply));
        if (eventHandler is null)
            throw new Exception("Domain event handler must exist");
        return eventHandler;
    }
}