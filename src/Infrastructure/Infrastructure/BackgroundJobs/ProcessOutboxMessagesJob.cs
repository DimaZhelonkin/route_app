using Ark.SharedLib.Application.Abstractions;
using Ark.SharedLib.Domain.Interfaces;
using Ark.SharedLib.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Polly;
using Quartz;

namespace Ark.Infrastructure.BackgroundJobs;

[DisallowConcurrentExecution]
public class ProcessOutboxMessagesJob : IJob
{
    private static readonly JsonSerializerSettings JsonSerializerSettings = new()
    {
        TypeNameHandling = TypeNameHandling.All,
    };

    private readonly IEnumerable<DbContext> _dbContexts;
    private readonly IEventBus _eventBus;
    private readonly ILogger<ProcessOutboxMessagesJob> _logger;
    private readonly IPublisher _publisher;

    public ProcessOutboxMessagesJob(
        IEnumerable<DbContext> dbContexts,
        IPublisher publisher,
        IEventBus eventBus,
        ILogger<ProcessOutboxMessagesJob> logger
    )
    {
        _dbContexts = dbContexts;
        _publisher = publisher;
        _eventBus = eventBus;
        _logger = logger;
    }

    #region IJob Members

    public async Task Execute(IJobExecutionContext context)
    {
        foreach (var dbContext in _dbContexts)
        {
            var outboxMessages = await dbContext
                                       .Set<OutboxMessage>()
                                       .Where(m => m.ProcessedOnUtc == null)
                                       .OrderBy(m => m.OccurredOnUtc)
                                       .Take(20)
                                       .ToListAsync(context.CancellationToken);

            foreach (var outboxMessage in outboxMessages)
            {
                var domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(
                    outboxMessage.Content,
                    JsonSerializerSettings
                );
                if (domainEvent is null)
                {
                    _logger.LogError(
                        "OutboxMessage {$OutboxMessage} has a content which couldn't be serialized as domain event",
                        outboxMessage);
                    continue;
                }

                var policy = Policy.Handle<Exception>()
                                   .WaitAndRetryAsync(
                                       3,
                                       attempt => TimeSpan.FromMicroseconds(50 * attempt)
                                   );
                var result = await policy.ExecuteAndCaptureAsync(() =>
                    _publisher.Publish(domainEvent, context.CancellationToken));
                // var result = await policy.ExecuteAndCaptureAsync(() =>
                // _eventBus.PublishAsync(domainEvent, context.CancellationToken));

                outboxMessage.PublishedOnUtc = result.FinalException is null ? DateTimeOffset.UtcNow : null;
                outboxMessage.Error = result.FinalException?.ToString();
                outboxMessage.ProcessedOnUtc = DateTimeOffset.UtcNow;
                
                dbContext.Update(outboxMessage);
            }

            if (outboxMessages.Any())
                await dbContext.SaveChangesAsync(context.CancellationToken);
        }
    }

    #endregion
}