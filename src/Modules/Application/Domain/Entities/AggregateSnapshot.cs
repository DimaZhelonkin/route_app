using Ark.SharedLib.Domain.Models.Entities;

namespace Ark.Application.Entities;

public class AggregateSnapshot : RemovableEntity<Guid>
{
    public AggregateSnapshot(Guid id) : base(id)
    {
    }

    public string AggregateId { get; set; }
    public string AggregateName { get; set; }
    public long LastAggregateVersion { get; set; }
    public Guid LastEventId { get; set; }
    public string Data { get; set; }
}