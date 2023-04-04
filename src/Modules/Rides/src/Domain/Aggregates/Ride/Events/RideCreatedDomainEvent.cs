using Ark.Rides.Domain.Aggregates.RideParticipant;
using Ark.SharedLib.Domain.Interfaces;
using Ark.SharedLib.Domain.Models.Events;

namespace Ark.Rides.Domain.Aggregates.Ride.Events;

/// <summary>
///     CreateRideDomainEvent
/// </summary>
public class RideCreatedDomainEvent<TRideId> : DomainEvent<RideCreatedDomainEvent<TRideId>, TRideId>
    where TRideId : RideId
{
    /// <summary>
    ///     Needed for serialization
    /// </summary>
    private RideCreatedDomainEvent()
    {
    }

    public RideCreatedDomainEvent(IRideParticipant participant, Guid routeId)
    {
        Participant = participant;
        RouteId = routeId;
    }

    public RideCreatedDomainEvent(TRideId aggregateId, long aggregateVersion,
        IRideParticipant participant, Guid routeId)
        : base(aggregateId, aggregateVersion)
    {
        Participant = participant;
        RouteId = routeId;
    }

    public IRideParticipant Participant { get; protected set; }
    public Guid RouteId { get; }

    public override RideCreatedDomainEvent<TRideId> WithAggregate(TRideId aggregateId, long aggregateVersion)
        => new(aggregateId, aggregateVersion, Participant, RouteId);
}