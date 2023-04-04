using Ark.Rides.Domain.Enums;
using Ark.SharedLib.Domain.Models.Events;

namespace Ark.Rides.Domain.Aggregates.Ride.Events;

/// <summary>
///     RideStatusChangeDomainEvent
/// </summary>
public class RideStatusChangedDomainEvent<TRideId> : DomainEvent<RideStatusChangedDomainEvent<TRideId>, TRideId>
    where TRideId : RideId
{
    /// <summary>
    ///     Needed for serialization
    /// </summary>
    private RideStatusChangedDomainEvent()
    {
    }

    public RideStatusChangedDomainEvent(RideStatus status)
    {
        Status = status;
    }

    public RideStatusChangedDomainEvent(TRideId aggregateId, long aggregateVersion, RideStatus status)
        : base(aggregateId, aggregateVersion)
    {
        Status = status;
    }

    public RideStatus Status { get; }

    public override RideStatusChangedDomainEvent<TRideId> WithAggregate(TRideId aggregateId, long aggregateVersion)
        => new(aggregateId, aggregateVersion, Status);
}