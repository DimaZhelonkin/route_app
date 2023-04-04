using Ark.Rides.Domain.Aggregates.DriverRide.Commands;
using Ark.Rides.Domain.Enums;
using Ark.SharedLib.Common.Exceptions;
using Ark.Vehicles.Aggregates;
using NetTopologySuite.Geometries;

namespace Ark.Rides.Domain.Aggregates.DriverRide;

public class DriverRideBuilder
{
    // Store specification state here
    private Driver.Driver? _driver;
    private Guid? _routeId;
    private VehicleId? _vehicleId;
    private DateTimeOffset? _finishedAt;
    private DateTimeOffset? _startedAt;
    private DateTimeOffset? _canceledAt;
    private LineString? _factRoute;
    private RideStatus _status = RideStatus.Created;
    private DriverRideId? _id;

    // Keep a collection of visitors
    // TODO add passengerRides

    // Prevent repeat builds
    private bool _isBuilt;


    public DriverRide Build()
    {
        ValidateState();
        var command = new CreateDriverRideCommand(_id!, _driver!, _routeId!.Value, _vehicleId!);
        var driverRide = DriverRide.Create(command);
        if (_routeId.HasValue)
            driverRide.RouteId = _routeId.Value;
        // TODO to finish it
        
        _isBuilt = true;
        return driverRide;
    }

    private void ValidateState()
    {
        if (_isBuilt) throw new IllegalStateException("The entity already has been built");

        IllegalStateException.ThrowIfNull(_id, "Id is required");
        IllegalStateException.ThrowIfNull(_driver, "The driver is required");
        IllegalStateException.ThrowIfNull(_routeId, "RouteId is required");
        IllegalStateException.ThrowIfNull(_vehicleId, "RouteId is required");
        // TODO check dependent props
    }

    public DriverRideBuilder WithId(DriverRideId id)
    {
        if (id.Value == default)
            throw new IllegalStateException("Id couldn't be default");
        _id = id;
        return this;
    }

    public DriverRideBuilder WithDriver(Driver.Driver driver)
    {
        _driver = driver;
        return this;
    }

    public DriverRideBuilder WithRoute(Guid routeId)
    {
        _routeId = routeId;
        return this;
    }

    public DriverRideBuilder WithVehicle(VehicleId vehicleId)
    {
        _vehicleId = vehicleId;
        return this;
    }

    public DriverRideBuilder WithStart(DateTimeOffset startDate)
    {
        _status = RideStatus.Started;
        _startedAt = startDate;
        return this;
    }

    public DriverRideBuilder WithFinish(DateTimeOffset finishDate)
    {
        _status = RideStatus.Finished;
        _finishedAt = finishDate;
        return this;
    }

    public static DriverRideBuilder CreateInstance() => new();
}