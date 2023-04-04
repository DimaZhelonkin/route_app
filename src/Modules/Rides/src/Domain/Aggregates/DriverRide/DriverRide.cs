using Ark.Rides.Domain.Aggregates.DriverRide.Commands;
using Ark.Rides.Domain.Aggregates.DriverRide.Events;
using Ark.Rides.Domain.Aggregates.DriverRide.Exceptions;
using Ark.Rides.Domain.Aggregates.DriverRide.Validations;
using Ark.Rides.Domain.Aggregates.Ride;
using Ark.Rides.Domain.Enums;
using Ark.SharedLib.Domain.Extensions;
using Ark.SharedLib.Domain.Interfaces;
using Ark.Vehicles.Aggregates;
using ViennaNET.Validation.Validators;

namespace Ark.Rides.Domain.Aggregates.DriverRide;

/// <summary>
///     Driver ride
/// </summary>
public class DriverRide : Ride<DriverRideId>,
    IDomainEventHandler<DriverRideCreatedEvent>,
    IDomainEventHandler<PassengerRideAddedEvent>,
    IDomainEventHandler<PassengerRideRemovedEvent>,
    IDomainEventHandler<DriverRideFinishedEvent>,
    IDomainEventHandler<DriverRideStartedEvent>
{
    private readonly List<PassengerRide.PassengerRide> _passengersRides = new();

    protected DriverRide(DriverRideId id) : base(id)
    {
    }

    private DriverRide(DriverRideId id, Driver.Driver driver, Guid routeId, VehicleId vehicleId) : base(id, driver,
        routeId)
    {
        RaiseEvent(new DriverRideCreatedEvent(driver, vehicleId, routeId));
    }

    public IReadOnlyList<PassengerRide.PassengerRide> PassengersRides => _passengersRides;

    public VehicleId VehicleId { get; private set; }

    /// <summary>
    ///     Creates a DriverRide
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public static DriverRide Create(CreateDriverRideCommand command)
    {
        var ruleSet = new CreateDriverRideRuleSet();
        RulesValidator.Validate(ruleSet, command)
                      .ThrowIfIsNotValid<CreateDriverRideException>();

        return new DriverRide(command.Id, command.Driver, command.RouteId, command.VehicleId);
    }

    public void AddPassengerRide(PassengerRide.PassengerRide driverRide) =>
        RaiseEvent(new PassengerRideAddedEvent(driverRide));

    public void RemovePassengerRide(PassengerRide.PassengerRide driverRide) =>
        RaiseEvent(new PassengerRideRemovedEvent(driverRide));

    /// <summary>
    /// </summary>
    /// <exception cref="StartDriverRideException"></exception>
    public override void Start()
    {
        var ruleSet = new StartDriverRideRuleSet();
        RulesValidator.Validate(ruleSet, this).ThrowIfIsNotValid<StartDriverRideException>();

        RaiseEvent(new DriverRideStartedEvent(DateTimeOffset.UtcNow));
    }

    /// <summary>
    /// </summary>
    /// <exception cref="Exception"></exception>
    public override void Finish()
    {
        var ruleSet = new FinishDriverRideRuleSet();
        RulesValidator.Validate(ruleSet, this).ThrowIfIsNotValid<FinishDriverRideException>();
        RaiseEvent(new DriverRideFinishedEvent(DateTimeOffset.UtcNow));
    }


    #region DomainEventHandlers

    void IDomainEventHandler<DriverRideCreatedEvent>.Apply(DriverRideCreatedEvent @event) =>
        VehicleId = @event.VehicleId;

    void IDomainEventHandler<PassengerRideAddedEvent>.Apply(PassengerRideAddedEvent @event) =>
        _passengersRides.Add(@event.PassengerRide);

    void IDomainEventHandler<PassengerRideRemovedEvent>.Apply(PassengerRideRemovedEvent @event) =>
        _passengersRides.Remove(@event.PassengerRide);

    void IDomainEventHandler<DriverRideStartedEvent>.Apply(DriverRideStartedEvent @event)
    {
        Status = RideStatus.Started;
        StartedAt = @event.StartDate;
    }

    void IDomainEventHandler<DriverRideFinishedEvent>.Apply(DriverRideFinishedEvent @event)
    {
        Status = RideStatus.Finished;
        FinishedAt = @event.FinishDate;
    }

    #endregion
}