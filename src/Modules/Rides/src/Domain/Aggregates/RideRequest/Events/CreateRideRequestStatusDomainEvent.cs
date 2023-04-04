using Ark.SharedLib.Domain.Interfaces;
using Ark.SharedLib.Domain.Models.Events;

namespace Ark.Rides.Domain.Aggregates.RideRequest.Events;

public class CreateRideRequestStatusDomainEvent : DomainEvent<Guid>
{
    /// <summary>
    ///     Needed for serialization
    /// </summary>
    private CreateRideRequestStatusDomainEvent()
    {
    }

    public CreateRideRequestStatusDomainEvent(DriverRide.DriverRide driverRide, PassengerRide.PassengerRide passengerRide)
    {
        DriverRide = driverRide;
        PassengerRide = passengerRide;
    }

    public CreateRideRequestStatusDomainEvent(Guid aggregateId, long aggregateVersion, DriverRide.DriverRide driverRide,
        PassengerRide.PassengerRide passengerRide) : base(
        aggregateId, aggregateVersion)
    {
        DriverRide = driverRide;
        PassengerRide = passengerRide;
    }

    public DriverRide.DriverRide DriverRide { get; }
    public PassengerRide.PassengerRide PassengerRide { get; }


    public override IDomainEvent<Guid> WithAggregate(Guid aggregateId, long aggregateVersion) =>
        new CreateRideRequestStatusDomainEvent(aggregateId, aggregateVersion, DriverRide, PassengerRide);
}