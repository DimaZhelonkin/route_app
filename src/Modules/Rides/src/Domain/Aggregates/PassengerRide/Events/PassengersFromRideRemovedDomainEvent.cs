using Ark.SharedLib.Domain.Interfaces;
using Ark.SharedLib.Domain.Models.Events;

namespace Ark.Rides.Domain.Aggregates.PassengerRide.Events;

/// <summary>
///     RemovedPassengersFromRideDomainEvent
/// </summary>
public class PassengersFromRideRemovedDomainEvent : DomainEvent<PassengerRideId>
{
    /// <summary>
    ///     Needed for serialization
    /// </summary>
    private PassengersFromRideRemovedDomainEvent()
    {
    }

    public PassengersFromRideRemovedDomainEvent(IEnumerable<Passenger.Passenger> passengers)
    {
        Passengers = passengers;
    }

    public PassengersFromRideRemovedDomainEvent(PassengerRideId aggregateId, long aggregateVersion,
        IEnumerable<Passenger.Passenger> passengers)
        : base(aggregateId, aggregateVersion)
    {
        Passengers = passengers;
    }

    public IEnumerable<Passenger.Passenger> Passengers { get; }

    public override IDomainEvent<PassengerRideId> WithAggregate(PassengerRideId aggregateId, long aggregateVersion) =>
        new PassengersFromRideRemovedDomainEvent(aggregateId, aggregateVersion, Passengers);
}