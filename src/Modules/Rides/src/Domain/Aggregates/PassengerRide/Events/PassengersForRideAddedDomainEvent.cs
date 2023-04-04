using Ark.SharedLib.Domain.Interfaces;
using Ark.SharedLib.Domain.Models.Events;

namespace Ark.Rides.Domain.Aggregates.PassengerRide.Events;

/// <summary>
///     AddPassengersForRideDomainEvent
/// </summary>
public class PassengersForRideAddedDomainEvent : DomainEvent<PassengerRideId>
{
    /// <summary>
    ///     Needed for serialization
    /// </summary>
    private PassengersForRideAddedDomainEvent()
    {
    }

    public PassengersForRideAddedDomainEvent(IEnumerable<Passenger.Passenger> passengers)
    {
        Passengers = passengers;
    }

    public PassengersForRideAddedDomainEvent(PassengerRideId aggregateId, long aggregateVersion, IEnumerable<Passenger.Passenger> passengers)
        : base(aggregateId, aggregateVersion)
    {
        Passengers = passengers;
    }

    public IEnumerable<Passenger.Passenger> Passengers { get; }

    public override IDomainEvent<PassengerRideId> WithAggregate(PassengerRideId aggregateId, long aggregateVersion) =>
        new PassengersForRideAddedDomainEvent(aggregateId, aggregateVersion, Passengers);
}