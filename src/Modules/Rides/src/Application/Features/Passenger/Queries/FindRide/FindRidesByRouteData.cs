using Ark.Rides.Application.Models;
using Ark.SharedLib.Common.Paging;

namespace Ark.Rides.Application.Features.Passenger.Queries.FindRide;

public class FindRidesByRouteData
{
    /// <summary>
    ///     Маршрут построенный из точки начала и конца отправленные пассажиром
    /// </summary>
    // TODO to LineString (in request make mapping)
    public string Route { get; set; }

    /// <summary>
    ///     Найденные поездки по построенному маршруту
    /// </summary>
    public PagedData<RideInfo> Rides { get; set; }
}