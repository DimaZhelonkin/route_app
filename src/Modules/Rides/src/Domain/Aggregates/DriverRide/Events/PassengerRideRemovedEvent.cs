using Ark.SharedLib.Domain.Interfaces;
using Ark.SharedLib.Domain.Models.Events;

namespace Ark.Rides.Domain.Aggregates.DriverRide.Events;

/// <summary>
///     Passenger Ride Removed Event
/// </summary>
public class PassengerRideRemovedEvent : DomainEvent<PassengerRideRemovedEvent, DriverRideId>
{
    /// <summary>
    ///     Needed for serialization
    /// </summary>
    private PassengerRideRemovedEvent()
    {
    }

    public PassengerRideRemovedEvent(PassengerRide.PassengerRide passengerRide)
    {
        PassengerRide = passengerRide;
    }

    public PassengerRideRemovedEvent(DriverRideId aggregateId, long aggregateVersion,
        PassengerRide.PassengerRide passengerRide)
        : base(aggregateId, aggregateVersion)
    {
        PassengerRide = passengerRide;
    }

    public PassengerRide.PassengerRide PassengerRide { get; }


    public override PassengerRideRemovedEvent WithAggregate(DriverRideId aggregateId, long aggregateVersion)
        => new(aggregateId, aggregateVersion, PassengerRide);
}