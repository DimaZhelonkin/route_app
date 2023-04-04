using Ark.IdentityServer.Domain.ApplicationUser;
using Ark.Rides.Domain.Aggregates.Passenger;

namespace Ark.Rides.UnitTests.Domain.DriverRideTests;

public static class TestPassengers
{
    public static Passenger Simple()
    {
        var passengerId = new PassengerId(Guid.NewGuid());
        var identityId = new IdentityId(passengerId.ToString());
        var passenger = Passenger.Create(passengerId, identityId);
        return passenger;
    }
}