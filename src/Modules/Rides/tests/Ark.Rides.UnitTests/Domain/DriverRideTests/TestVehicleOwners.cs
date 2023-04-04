using Ark.IdentityServer.Domain.ApplicationUser;
using Ark.Rides.Domain.Aggregates.Driver;
using Ark.Vehicles.Aggregates;

namespace Ark.Rides.UnitTests.Domain.DriverRideTests;

public static class TestVehicleOwners
{
    public static VehicleOwner Simple(VehicleOwnerId? ownerId = null, IdentityId? identityId = null)
    {
        Driver? driver = null;
        if (ownerId is null || identityId == null)
            driver = TestDrivers.Simple();
        var vehicleOwnerId = driver?.Id is null ? ownerId : new VehicleOwnerId(driver.Id.Value);
        var vehicleOwnerIdentityId = driver?.IdentityId ?? identityId;
        var vehicleOwner = VehicleOwner.Create(vehicleOwnerId, vehicleOwnerIdentityId!);
        return vehicleOwner;
    }
}