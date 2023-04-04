using Ark.Rides.Domain.Aggregates.DriverRide;
using Ark.Rides.Domain.Aggregates.DriverRide.Commands;
using Ark.Vehicles.Aggregates;

namespace Ark.Rides.UnitTests.Domain.DriverRideTests;

public static class TestDriverRides
{
    public static DriverRide Simple()
    {
        var driver = TestDrivers.Simple();
        var driverRideId = new DriverRideId(Guid.NewGuid());
        var routeId = Guid.NewGuid();
        var vehicleOwnerId = new VehicleOwnerId(driver.Id.Value);
        var vehicleOwner = TestVehicleOwners.Simple(vehicleOwnerId, driver.IdentityId);
        var vehicle = TestVehicles.Simple(vehicleOwner);
        var createDriverRideCommand = new CreateDriverRideCommand(driverRideId, driver, routeId, vehicle.Id);
        var driverRide = DriverRide.Create(createDriverRideCommand);
        return driverRide;
    }
}