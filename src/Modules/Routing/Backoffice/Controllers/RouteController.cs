using Ark.Core.Models;
using Ark.Infrastructure.Shared.Controllers;
using Ark.Infrastructure.Shared.Result.AspNetCore;
using Ark.Routing.Features.Queries.GetRoute;
using Ark.Routing.Models.Enums;
using Ark.SharedLib.Common.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ark.Routing.Controllers;

/// <summary>
/// </summary>
[Area("Routing")]
[Route("[area]/[controller]")]
public class RouteController : ApiBaseController
{
    /// <summary>
    ///     Get route
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(typeof(GetRouteResponse), StatusCodes.Status200OK)]
    public Task<Result<GetRouteResponse>> GetRoute(CancellationToken cancellationToken)
    {
        var query = new GetRouteRequest
        {
            Coordinates = new List<Point>
            {
                new(43.133200, 131.911300),
                new(50.266000, 127.535600),
            },
            Id = null,
            Costing = CostingTransportType.Auto,
            DateTimeType = RoutingDateTimeType.Default,
        };
        return Mediator.Send(query, cancellationToken);
    }

    /// <summary>
    ///     Get route by id
    /// </summary>
    /// <param name="id">route identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(typeof(GetRouteResponse), StatusCodes.Status200OK)]
    public Task<Result<GetRouteResponse>> GetRouteById([FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var query = new GetRouteRequest
        {
            Coordinates = new List<Point>
            {
                new(43.133200, 131.911300),
                new(50.266000, 127.535600),
            },
            Id = id,
            Costing = CostingTransportType.Auto,
            DateTimeType = RoutingDateTimeType.Default,
        };
        return Mediator.Send(query, cancellationToken);
    }
}