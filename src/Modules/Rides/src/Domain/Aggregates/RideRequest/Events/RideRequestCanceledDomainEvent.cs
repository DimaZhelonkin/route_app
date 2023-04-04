using Ark.SharedLib.Domain.Models.Events;

namespace Ark.Rides.Domain.Aggregates.RideRequest.Events;

public class RideRequestCanceledDomainEvent : DomainEvent<RideRequestCanceledDomainEvent, RideRequestId>
{
    /// <summary>
    ///     Needed for serialization
    /// </summary>
    public RideRequestCanceledDomainEvent()
    {
    }

    public RideRequestCanceledDomainEvent(RideRequestId aggregateId, long aggregateVersion) : base(aggregateId,
        aggregateVersion)
    {
    }

    public override RideRequestCanceledDomainEvent WithAggregate(RideRequestId aggregateId, long aggregateVersion) =>
        new(aggregateId, aggregateVersion);
}