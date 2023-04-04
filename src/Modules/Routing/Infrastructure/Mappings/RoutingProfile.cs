using Ark.Routing.HttpClients.VkClient.Models.RequestModels.Enums;
using RoutingPoint = Ark.Routing.HttpClients.VkClient.Models.RequestModels.Point;
using AutoMapper;
using TopologyPoint = NetTopologySuite.Geometries.Point;
using ApplicationEnums = Ark.Routing.Models.Enums;
namespace Ark.Routing.Mappings;

public class RoutingProfile : Profile
{
    public RoutingProfile()
    {
        CreateMap<TopologyPoint, RoutingPoint>()
            .ForMember(
                dest => dest.Lat,
                opt => opt.MapFrom(src => src.Y)
            )
            .ForMember(
                dest => dest.Lon,
                opt => opt.MapFrom(src => src.X)
            )
            .ForMember(
                dest => dest.Type,
                opt => opt.MapFrom(src => RoutingType.Break)
            );
        CreateMap<ApplicationEnums.CostingTransportType, CostingTransportType>();
        CreateMap<ApplicationEnums.RoutingDateTimeType, RoutingDateTimeType>();
    }
}