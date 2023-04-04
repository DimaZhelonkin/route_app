using Ark.IdentityServer.Domain.ApplicationUser;
using Ark.Rides.Domain.Aggregates.Driver;

namespace Ark.Rides.UnitTests.Domain.DriverRideTests;

public static class TestDrivers
{
    public static Driver Simple()
    {
        var driverId = new DriverId(Guid.NewGuid());
        var driverIdentityId = new IdentityId(driverId.ToString());
        var driver = Driver.Create(driverId, driverIdentityId);
        return driver;
    }
}