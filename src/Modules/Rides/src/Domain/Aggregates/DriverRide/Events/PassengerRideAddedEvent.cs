using Ark.Rides.Domain.Aggregates.PassengerRide;
using Ark.SharedLib.Domain.Models.Events;

namespace Ark.Rides.Domain.Aggregates.DriverRide.Events;

/// <summary>
///     Passenger Ride Added Event
/// </summary>
public class PassengerRideAddedEvent : DomainEvent<PassengerRideAddedEvent, DriverRideId>
{
    /// <summary>
    ///     Needed for serialization
    /// </summary>
    private PassengerRideAddedEvent()
    {
    }

    public PassengerRideAddedEvent(PassengerRide.PassengerRide passengerRide)
    {
        PassengerRide = passengerRide;
    }

    public PassengerRideAddedEvent(DriverRideId aggregateId, long aggregateVersion, PassengerRide.PassengerRide passengerRide)
        : base(aggregateId, aggregateVersion)
    {
        PassengerRide = passengerRide;
    }

    public PassengerRide.PassengerRide PassengerRide { get; }

    public override PassengerRideAddedEvent WithAggregate(DriverRideId aggregateId, long aggregateVersion) =>
        new(aggregateId, aggregateVersion, PassengerRide);
}