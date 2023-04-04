using Ark.Vehicles.Aggregates;

namespace Ark.Rides.UnitTests.Domain.DriverRideTests;

public static class TestVehicles
{
    public static Vehicle Simple(VehicleOwner? vehicleOwner = null)
    {
        vehicleOwner ??= TestVehicleOwners.Simple();
        var vehicleId = new VehicleId(Guid.NewGuid());
        var licensePlate = Guid.NewGuid().ToString();
        var vehicle = Vehicle.Create(vehicleId, vehicleOwner, licensePlate);
        return vehicle;
    }
}