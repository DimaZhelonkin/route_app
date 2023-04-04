using Ark.SharedLib.Domain.Interfaces;
using Ark.SharedLib.Domain.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;

namespace Ark.SharedLib.Persistence.Interceptors;

public sealed class ConvertDomainEventsToOutboxMessagesInterceptor : SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var dbContext = eventData.Context;
        if (dbContext is null)
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        var aggregateRoots = dbContext.ChangeTracker
                                      .Entries<IAggregateRoot>()
                                      .Select(x => x.Entity)
                                      .ToList();
        var domainEvents = aggregateRoots
                           .SelectMany(aggregateRoot => aggregateRoot.GetDomainEvents())
                           .ToList();
        aggregateRoots.ForEach(aggregateRoot => aggregateRoot.ClearDomainEvents());

        var outboxMessages = domainEvents
                             .Select(domainEvent => new OutboxMessage
                             {
                                 Id = Guid.NewGuid(),
                                 OccurredOnUtc = DateTimeOffset.UtcNow,
                                 Type = domainEvent.GetType().FullName ?? domainEvent.GetType().Name,
                                 Content = JsonConvert.SerializeObject(
                                     domainEvent,
                                     new JsonSerializerSettings
                                     {
                                         TypeNameHandling = TypeNameHandling.All,
                                     }),
                             })
                             .ToList();
        await dbContext.Set<OutboxMessage>().AddRangeAsync(outboxMessages, cancellationToken);

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}