using Ark.Routing.Features.GetRouteQuery;
using Ark.Routing.Features.Queries.GetRoute;
using AutoMapper;
using NetTopologySuite.Geometries;
using Point = Ark.Core.Models.Point;

namespace Ark.Routing.Mappings;

public class RoutingProfile : Profile
{
    public RoutingProfile()
    {
        CreateMap<GetRouteRequest, GetRouteQuery>();
        CreateMap<GetRouteResult, GetRouteResponse>();
        CreateMap<Point, Coordinate>().ConstructUsing(p => new Coordinate(p.Lat, p.Lon));
    }
}