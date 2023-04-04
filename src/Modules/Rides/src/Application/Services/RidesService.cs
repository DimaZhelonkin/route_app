using Ark.Rides.Application.Contracts;

namespace Ark.Rides.Application.Services;

public class RidesService : IRidesService
{
    #region IRidesService Members

    public Task<decimal> GetRecommendedPrice(Guid routeId) => throw new NotImplementedException();

    #endregion

    public void Test()
    {
        // var driver = new Driver();
        // // var vehicle = new Vehicle();
        // var coordinates = new Coordinate[]
        // {
        //     new(0, 0),
        //     new(0, 1)
        // };
        // var currentRoute = new LineString(coordinates);
        // var route = new Route(currentRoute);
        // var driverRide = new DriverRide(driver, route.Id, vehicle.Id);
    }
}