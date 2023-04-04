using Ark.SharedLib.Domain.Models.Events;

namespace Ark.Rides.Domain.Aggregates.PassengerRide.Events;

/// <summary>
///     RemovedPassengerFromRideDomainEvent
/// </summary>
public class PassengerFromRideRemovedDomainEvent : DomainEvent<PassengerFromRideRemovedDomainEvent, PassengerRideId>
{
    /// <summary>
    ///     Needed for serialization
    /// </summary>
    private PassengerFromRideRemovedDomainEvent()
    {
    }

    public PassengerFromRideRemovedDomainEvent(Passenger.Passenger passenger)
    {
        Passenger = passenger;
    }

    public PassengerFromRideRemovedDomainEvent(PassengerRideId aggregateId, long aggregateVersion,
        Passenger.Passenger passenger)
        : base(aggregateId, aggregateVersion)
    {
        Passenger = passenger;
    }

    public Passenger.Passenger Passenger { get; }

    public override PassengerFromRideRemovedDomainEvent WithAggregate(PassengerRideId aggregateId,
        long aggregateVersion) => new(aggregateId, aggregateVersion, Passenger);
}