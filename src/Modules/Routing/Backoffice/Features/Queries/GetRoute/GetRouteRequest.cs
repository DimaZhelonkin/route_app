using Ark.Core.Models;
using Ark.Routing.Models.Enums;
using Ark.SharedLib.Common.CQS.Implementations;

namespace Ark.Routing.Features.Queries.GetRoute;

public class GetRouteRequest : QueryResult<GetRouteResponse>
{
    public List<Point> Coordinates { get; set; }
    public Guid? Id { get; set; }
    public CostingTransportType Costing { get; set; }
    public RoutingDateTimeType DateTimeType { get; set; }
}