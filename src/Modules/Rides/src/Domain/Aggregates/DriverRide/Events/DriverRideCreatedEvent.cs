using Ark.SharedLib.Domain.Interfaces;
using Ark.SharedLib.Domain.Models.Events;
using Ark.Vehicles.Aggregates;

namespace Ark.Rides.Domain.Aggregates.DriverRide.Events;

/// <summary>
///     CreateRideDomainEvent
/// </summary>
public class DriverRideCreatedEvent : DomainEvent<DriverRideId>
{
    public DriverRideCreatedEvent(Driver.Driver driver,
        VehicleId vehicleId,
        Guid routeId)
    {
        Driver = driver;
        VehicleId = vehicleId;
        RouteId = routeId;
    }

    public DriverRideCreatedEvent(DriverRideId aggregateId, long aggregateVersion,
        Driver.Driver driver,
        VehicleId vehicleId,
        Guid routeId)
        : base(aggregateId, aggregateVersion)
    {
        Driver = driver;
        VehicleId = vehicleId;
        RouteId = routeId;
    }

    public VehicleId VehicleId { get; }
    public Guid RouteId { get; }
    public Driver.Driver Driver { get; }

    /// <summary>
    /// </summary>
    /// <param name="aggregateId"></param>
    /// <param name="aggregateVersion"></param>
    /// <returns></returns>
    public override IDomainEvent<DriverRideId> WithAggregate(DriverRideId aggregateId, long aggregateVersion) =>
        new DriverRideCreatedEvent(aggregateId, aggregateVersion, Driver, VehicleId, RouteId);
}