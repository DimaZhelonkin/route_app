using Ark.Routing.Aggregates;
using Ark.Routing.Contracts;
using Ark.Routing.Features.GetRouteQuery;
using Ark.Routing.HttpClients.VkClient;
using Ark.Routing.HttpClients.VkClient.Models;
using Ark.Routing.HttpClients.VkClient.Models.ResponseModels;
using Ark.Routing.Services.Options;
using Ark.SharedLib.Common.Results;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetTopologySuite.Geometries;

namespace Ark.Routing.Services;

public class RoutingProvider : IRoutingProvider
{
    private readonly ILogger<RoutingProvider> _logger;
    private readonly IMapper _mapper;
    private readonly IOptions<RoutingOptions> _options;
    private readonly IVkRoutingClient _vkRoutingClient;

    public RoutingProvider(IVkRoutingClient vkRoutingClient, ILogger<RoutingProvider> logger, IMapper mapper,
        IOptions<RoutingOptions> options)
    {
        _vkRoutingClient = vkRoutingClient;
        _logger = logger;
        _mapper = mapper;
        _options = options;
    }

    #region IRoutingProvider Members

    public async Task<Result<Route>> GetRouteAsync(GetRouteQuery query, CancellationToken cancellationToken = default)
    {
        var pointsArray = query.Coordinates.Select(x => new Point(x));
        try
        {
            var mappedPoint = _mapper.Map<List<HttpClients.VkClient.Models.RequestModels.Point>>(pointsArray);
            var request = new DirectionsRequest(query, mappedPoint, _options);
            var response = await _vkRoutingClient.DirectionsAsync(request, cancellationToken);
            if (response.Value?.Trips is null)
                throw new Exception($"response was invalid or were fallback with error {response?.Value?.Error}");
            var coordinates = GetCoordinatesFromTrips(response.Value.Trips!);
            var route = new Route(new Guid(), new LineString(coordinates));
            return Result.Success(route);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error getting something fun to say: {Error}", ex);
            throw;
        }
    }

    #endregion

    private Coordinate[] GetCoordinatesFromTrips(IEnumerable<Trip> trips) =>
        trips
            .SelectMany(trip => trip.Locations.Select(location => new Coordinate(location.Lat, location.Lon)))
            .ToArray();
}