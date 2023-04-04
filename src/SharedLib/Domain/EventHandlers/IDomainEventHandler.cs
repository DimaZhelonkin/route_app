using Ark.SharedLib.Domain.Interfaces;
using MediatR;

namespace Ark.SharedLib.Domain.EventHandlers;

public interface IDomainEventHandler<in T> : INotificationHandler<T> where T : IDomainEvent
{
    Task Handle(T @event, CancellationToken cancellationToken);
}