using MediatR;

namespace Ark.SharedLib.Domain.Interfaces;

public interface IDomainEventHandler<in T> : INotification where T : IDomainEvent
{
    void Apply(T @event);
}