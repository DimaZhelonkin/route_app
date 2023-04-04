using Ardalis.GuardClauses;
using Ark.Rides.Domain.Aggregates.PassengerRide.Events;
using Ark.Rides.Domain.Aggregates.Ride;
using Ark.Rides.Domain.Enums;
using Ark.SharedLib.Domain.Interfaces;

namespace Ark.Rides.Domain.Aggregates.PassengerRide;

public partial record PassengerRideId(Guid Value) : RideId(Value);

public sealed class PassengerRide : Ride<PassengerRideId>,
    IDomainEventHandler<PassengerRideCreatedDomainEvent>,
    IDomainEventHandler<PassengerRideStartedDomainEvent>,
    IDomainEventHandler<PassengerRideFinishedDomainEvent>
{
    private readonly List<DriverRide.DriverRide> _driversRides = new();

    private PassengerRide(PassengerRideId id) : base(id)
    {
    }

    private PassengerRide(PassengerRideId id, Passenger.Passenger passenger, Guid routeId) : base(id, passenger, routeId)
    {
        RaiseEvent(new PassengerRideCreatedDomainEvent(passenger, routeId));
    }

    public static PassengerRide Create(PassengerRideId id, Passenger.Passenger passenger, Guid routeId)
    {
        return new PassengerRide(id, passenger, routeId);
    }

    public IEnumerable<DriverRide.DriverRide> DriversRides => _driversRides;

    public Passenger.Passenger Passenger
    {
        get => (Passenger.Passenger)Participant;
        private set => Participant = value;
    }

    public void AddDriverRide(DriverRide.DriverRide driverRide)
    {
        Guard.Against.Null(driverRide);

        _driversRides.Add(driverRide);
    }

    public void RemoveDriverRide(DriverRide.DriverRide driverRide)
    {
        Guard.Against.Null(driverRide);

        _driversRides.Remove(driverRide);
    }

    public override void Start()
    {
        // TODO validate
        if (Status < RideStatus.Started)
            throw new Exception();
        RaiseEvent(new PassengerRideStartedDomainEvent(DateTimeOffset.UtcNow));
    }

    public override void Finish()
    {
        if (Status < RideStatus.OnTheWay)
            throw new Exception();
        RaiseEvent(new PassengerRideFinishedDomainEvent(DateTimeOffset.UtcNow));
    }

    #region Domain Event Handlers

    void IDomainEventHandler<PassengerRideStartedDomainEvent>.Apply(PassengerRideStartedDomainEvent @event)
    {
        Status = RideStatus.Started;
        StartedAt = @event.StartDate;
    }

    void IDomainEventHandler<PassengerRideFinishedDomainEvent>.Apply(PassengerRideFinishedDomainEvent @event)
    {
        Status = RideStatus.Finished;
        FinishedAt = DateTimeOffset.UtcNow;
    }

    void IDomainEventHandler<PassengerRideCreatedDomainEvent>.Apply(PassengerRideCreatedDomainEvent @event)
    {
        RouteId = @event.RouteId;
        Passenger = @event.Passenger;
    }

    #endregion
}