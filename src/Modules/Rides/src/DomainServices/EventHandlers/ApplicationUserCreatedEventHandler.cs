using Ark.IdentityServer.Domain.ApplicationUser.Events;
using Ark.SharedLib.Domain.EventHandlers;

namespace Ark.Rides.DomainServices.EventHandlers;

public class ApplicationUserCreatedEventHandler : IDomainEventHandler<ApplicationUserCreatedEvent>
{
    #region IDomainEventHandler<ApplicationUserCreatedEvent> Members

    public Task Handle(ApplicationUserCreatedEvent @event, CancellationToken cancellationToken) => Task.CompletedTask;

    #endregion
}