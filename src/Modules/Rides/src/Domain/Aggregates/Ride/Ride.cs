using Ardalis.GuardClauses;
using Ark.Rides.Domain.Aggregates.Ride.Events;
using Ark.Rides.Domain.Aggregates.RideParticipant;
using Ark.Rides.Domain.Enums;
using Ark.SharedLib.Domain.Interfaces;
using Ark.SharedLib.Domain.Models.Entities;
using Ark.StronglyTypedIds;
using NetTopologySuite.Geometries;

namespace Ark.Rides.Domain.Aggregates.Ride;

public abstract class Ride<TRideId> : AggregateRootBase<TRideId>,
    IDomainEventHandler<RideCreatedDomainEvent<TRideId>>,
    IDomainEventHandler<RideStatusChangedDomainEvent<TRideId>>
    where TRideId : RideId
{
    private RideStatus _status;

    protected Ride(TRideId id) : base(id)
    {
    }

    protected Ride(TRideId id, IRideParticipant participant, Guid routeId) : base(id)
    {
        Guard.Against.Null(participant);
        Guard.Against.Null(routeId);

        RaiseEvent(new RideCreatedDomainEvent<TRideId>(participant, routeId));
    }

    protected IRideParticipant Participant { get; set; }
    public Guid RouteId { get; set; }
    public LineString? FactRoute { get; set; }

    public DateTimeOffset? StartedAt { get; protected set; }
    public DateTimeOffset? FinishedAt { get; protected set; }
    public DateTimeOffset? CanceledAt { get; protected set; }

    public RideStatus Status
    {
        get => _status;
        protected set => RaiseEvent(new RideStatusChangedDomainEvent<TRideId>(value));
    }

    public abstract void Start();
    public abstract void Finish();

    #region IDomainEventHandlers

    void IDomainEventHandler<RideCreatedDomainEvent<TRideId>>.Apply(RideCreatedDomainEvent<TRideId> @event)
    {
        Participant = @event.Participant;
        RouteId = @event.RouteId;
    }

    void IDomainEventHandler<RideStatusChangedDomainEvent<TRideId>>.Apply(RideStatusChangedDomainEvent<TRideId> @event) =>
        _status = @event.Status;

    #endregion
}

public partial record RideId(Guid Value) : StronglyTypedId<Guid>(Value);