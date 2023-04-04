using Ark.SharedLib.Domain.Interfaces;
using Ark.SharedLib.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ark.Infrastructure.Idempotence;

public class
    IdempotentDomainEventHandler<TDomainEvent> : SharedLib.Domain.EventHandlers.IDomainEventHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent
{
    private readonly ApplicationDbContext _dbContext;
    private readonly INotificationHandler<TDomainEvent> _decorated;

    public IdempotentDomainEventHandler(INotificationHandler<TDomainEvent> decorated, ApplicationDbContext dbContext)
    {
        _decorated = decorated;
        _dbContext = dbContext;
    }

    #region IDomainEventHandler<TDomainEvent> Members

    public async Task Handle(TDomainEvent @event, CancellationToken cancellationToken)
    {
        var consumer = _decorated.GetType().Name;
        if (await _dbContext.Set<OutboxMessageConsumer>()
                            .AnyAsync(
                                outboxMessageConsumer =>
                                    outboxMessageConsumer.Id == @event.EventId &&
                                    outboxMessageConsumer.Name == consumer,
                                cancellationToken))
            return;

        await _decorated.Handle(@event, cancellationToken);

        await _dbContext.Set<OutboxMessageConsumer>()
                        .AddAsync(new OutboxMessageConsumer
                        {
                            Id = @event.EventId,
                            Name = consumer,
                        }, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    #endregion
}