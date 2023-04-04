using Ark.Routing.HttpClients.VkClient.Models;
using Ark.SharedLib.Common.Results;

namespace Ark.Routing.HttpClients.VkClient;

public interface IVkRoutingClient
{
    // Task<LineString> GetRoute(Point startPoint, Point finishPoint);
    Task<Result<DirectionsResponse>> DirectionsAsync(DirectionsRequest request, CancellationToken cancellationToken = default);
}