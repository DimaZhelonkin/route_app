using Ark.Core.Models;
using Ark.Rides.Application.Models;
using Ark.SharedLib.Common.CQS.Implementations;

namespace Ark.Rides.Application.Features.Passenger.Queries.FindRide;

/// <summary>
/// </summary>
public class FindRidesByRouteQuery : QueryResult<FindRidesByRouteData>
{
    public Point StartPoint { get; set; }
    public Point FinishPoint { get; set; }
    public RideSettings Settings { get; set; }
}