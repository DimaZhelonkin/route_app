using Ark.SharedLib.Domain.Models.Events;

namespace Ark.Rides.Domain.Aggregates.RideRequest.Events;

public class RideRequestConfirmedDomainEvent : DomainEvent<RideRequestConfirmedDomainEvent, RideRequestId>
{
    /// <summary>
    ///     Needed for serialization
    /// </summary>
    public RideRequestConfirmedDomainEvent()
    {
    }

    public RideRequestConfirmedDomainEvent(RideRequestId aggregateId, long aggregateVersion) : base(aggregateId,
        aggregateVersion)
    {
    }

    public override RideRequestConfirmedDomainEvent WithAggregate(RideRequestId aggregateId, long aggregateVersion) =>
        new(aggregateId, aggregateVersion);
}