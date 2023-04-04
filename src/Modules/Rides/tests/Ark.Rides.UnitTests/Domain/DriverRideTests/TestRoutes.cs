using Ark.Routing.Aggregates;
using Ark.Vehicles.Aggregates;
using NetTopologySuite.Geometries;

namespace Ark.Rides.UnitTests.Domain.DriverRideTests;

public static class TestRoutes
{
    public static Route Simple(VehicleOwner? vehicleOwner = null)
    {
        var startPoint = new Point(0, 0);
        var destinationPoint = new Point(10, 10);
        var routeId = Guid.NewGuid();
        var coordinates = new Coordinate[]
        {
            new(startPoint.X, startPoint.Y),
            new(destinationPoint.X, destinationPoint.Y),
        };
        var lineString = new LineString(coordinates);
        var route = new Route(routeId, lineString);
        return route;
    }
}