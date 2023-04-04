using Ark.Core.Models;
using Ark.Rides.Application.Features.Passenger.Queries.FindRide;
using Ark.Rides.Application.Models;
using Ark.SharedLib.Common.CQS.Implementations;

namespace Ark.Rides.Backoffice.Features.Passenger.Queries.FindRide;

/// <summary>
///     Найти попутчика
///     Создается фоновая задача по поиску попутчика - нет ответа
/// </summary>
public class FindRidesByRouteRequest : QueryResult<FindRidesByRouteData>
{
    public Point StartPoint { get; set; }
    public Point FinishPoint { get; set; }
    public RideSettings Settings { get; set; }
}