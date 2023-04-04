using Ark.Routing.Aggregates;
using Ark.Routing.Features.GetRouteQuery;
using Ark.SharedLib.Common.Results;
using NetTopologySuite.Geometries;

namespace Ark.Routing.Contracts;

public interface IRoutingProvider
{
    /// <summary>
    ///     Makes request to VK via VKClient, gets LineString and returns Route
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Result<Route>> GetRouteAsync(GetRouteQuery query, CancellationToken cancellationToken = default);

    Task<Result<Route>> GetRouteAsync(Point startPoint, Point destinationPoint, CancellationToken cancellationToken = default)
        => GetRouteAsync(
            new GetRouteQuery
            {
                Coordinates = new List<Coordinate>
                {
                    new(startPoint.X, startPoint.Y),
                    new(destinationPoint.X, destinationPoint.Y),
                },
            },
            cancellationToken);
}