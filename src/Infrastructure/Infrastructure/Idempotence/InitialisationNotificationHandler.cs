using MediatR;

namespace Ark.Infrastructure.Idempotence;

public class InitialisationNotificationHandler : INotificationHandler<InitialisationNotification>
{
    public Task Handle(InitialisationNotification notification, CancellationToken cancellationToken) =>
        Task.CompletedTask;
}