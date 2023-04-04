using Ark.IdentityServer.Domain.ApplicationUser.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ark.IdentityServer.Infrastructure.EventHandlers;

public class ApplicationUserCreatedEventConsumer : IConsumer<ApplicationUserCreatedEvent>
{
    private readonly ILogger<ApplicationUserCreatedEventConsumer> _logger;
    private readonly IPublisher _publisher;

    public ApplicationUserCreatedEventConsumer(ILogger<ApplicationUserCreatedEventConsumer> logger,
        IPublisher publisher)
    {
        _logger = logger;
        _publisher = publisher;
    }

    #region IConsumer<ApplicationUserCreatedEvent> Members

    public async Task Consume(ConsumeContext<ApplicationUserCreatedEvent> context)
    {
        _logger.LogInformation("Test get event from message queue, {@Message}", context.Message);
        await _publisher.Publish(context.Message, context.CancellationToken);
    }

    #endregion
}