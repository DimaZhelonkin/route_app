using Ark.SharedLib.Domain.Models.Events;

namespace Ark.Rides.Domain.Aggregates.RideRequest.Events;

public class RideRequestRejectDomainEvent : DomainEvent<RideRequestRejectDomainEvent, RideRequestId>
{
    /// <summary>
    ///     Needed for serialization
    /// </summary>
    public RideRequestRejectDomainEvent()
    {
    }

    public RideRequestRejectDomainEvent(RideRequestId aggregateId, long aggregateVersion) : base(aggregateId,
        aggregateVersion)
    {
    }

    public override RideRequestRejectDomainEvent WithAggregate(RideRequestId aggregateId, long aggregateVersion) =>
        new(aggregateId, aggregateVersion);
}