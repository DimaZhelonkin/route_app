using Ark.SharedLib.Domain.Models.Events;

namespace Ark.Rides.Domain.Aggregates.PassengerRide.Events;

/// <summary>
///     CreatePassengerRideDomainEvent
/// </summary>
public class PassengerRideCreatedDomainEvent : DomainEvent<PassengerRideCreatedDomainEvent, PassengerRideId>
{
    public PassengerRideCreatedDomainEvent(Passenger.Passenger passenger, Guid routeId)
    {
        Passenger = passenger;
        RouteId = routeId;
    }

    public PassengerRideCreatedDomainEvent(PassengerRideId aggregateId, long aggregateVersion,
        Passenger.Passenger passenger,
        Guid routeId)
        : base(aggregateId, aggregateVersion)
    {
        Passenger = passenger;
        RouteId = routeId;
    }

    public Passenger.Passenger Passenger { get; }
    public Guid RouteId { get; }


    public override PassengerRideCreatedDomainEvent WithAggregate(PassengerRideId aggregateId, long aggregateVersion) =>
        new(aggregateId, aggregateVersion, Passenger, RouteId);
}