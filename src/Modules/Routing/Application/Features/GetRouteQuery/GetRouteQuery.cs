using Ark.Routing.Models.Enums;
using Ark.SharedLib.Common.CQS.Implementations;
using NetTopologySuite.Geometries;

namespace Ark.Routing.Features.GetRouteQuery;

public class GetRouteQuery : QueryResult<GetRouteResult>
{
    public List<Coordinate> Coordinates { get; set; }
    public string Id { get; set; }
    public CostingTransportType Costing { get; set; }
    public RoutingDateTimeType DateTimeType { get; set; }
    // var coordinates = new List<Coordinate>
    // {
    //     new(43.133200, 131.911300),
    //     new(50.266000, 127.535600)
    // };
}