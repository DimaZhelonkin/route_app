using Ark.SharedLib.Application.Abstractions;
using MassTransit;

namespace Ark.Infrastructure.MessageBroker;

public sealed class EventBus : IEventBus
{
    private readonly IPublishEndpoint _publishEndpoint;

    public EventBus(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    #region IEventBus Members

    public Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : class =>
        _publishEndpoint.Publish(message, message.GetType(), cancellationToken);

    #endregion
}