using Ark.SharedLib.Domain.Interfaces;
using Ark.SharedLib.Domain.Models.Events;

namespace Ark.Rides.Domain.Aggregates.RideRequest.Events;

public class RideRequestStatusChangedDomainEvent : DomainEvent<RideRequestStatusChangedDomainEvent, RideRequestId>
{
    /// <summary>
    ///     Needed for serialization
    /// </summary>
    private RideRequestStatusChangedDomainEvent()
    {
    }

    public RideRequestStatusChangedDomainEvent(RideRequestStatus status)
    {
        Status = status;
    }

    public RideRequestStatusChangedDomainEvent(RideRequestId aggregateId, long aggregateVersion,
        RideRequestStatus status) : base(
        aggregateId, aggregateVersion)
    {
        Status = status;
    }

    public RideRequestStatus Status { get; }

    public override RideRequestStatusChangedDomainEvent WithAggregate(RideRequestId aggregateId,
        long aggregateVersion) => new(aggregateId, aggregateVersion, Status);
}