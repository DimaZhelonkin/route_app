using Ark.Core.Models;
using Ark.Rides.Application.Features.Common.Commands.CancelRide;
using Ark.Rides.Application.Features.Driver.Commands.ConfirmMatch;
using Ark.Rides.Application.Features.Driver.Commands.CreateDriverRide;
using Ark.Rides.Application.Features.Passenger.Commands.CancelMatch;
using Ark.Rides.Application.Features.Passenger.Commands.SendMatch;
using Ark.Rides.Application.Features.Passenger.Queries.FindRide;
using Ark.Rides.Application.Features.Passenger.Queries.GetRecommendedPrice;
using Ark.Rides.Application.Features.Passenger.Queries.GetRideInfo;
using Ark.Rides.Backoffice.Features.Driver.Commands.CancelRide;
using Ark.Rides.Backoffice.Features.Driver.Commands.ConfirmMatch;
using Ark.Rides.Backoffice.Features.Driver.Commands.CreateDriverRide;
using Ark.Rides.Backoffice.Features.Passenger.Commands.CancelMatch;
using Ark.Rides.Backoffice.Features.Passenger.Commands.SendMatch;
using Ark.Rides.Backoffice.Features.Passenger.Queries.FindRide;
using Ark.Rides.Backoffice.Features.Passenger.Queries.GetRecommendedPriceByRoute;
using Ark.Rides.Backoffice.Features.Passenger.Queries.GetRideInfo;
using AutoMapper;

namespace Ark.Rides.Backoffice.Mappings;

public class RidesProfile : Profile
{
    public RidesProfile()
    {
        CreateMap<Point, NetTopologySuite.Geometries.Point>()
            .ConstructUsing(x => new NetTopologySuite.Geometries.Point(x.Lat, x.Lon));

        #region Driver

        #region Commands

        CreateMap<CreateDriverRideRequest, CreateDriverRideCommand>();
        CreateMap<ConfirmMatchRequest, ConfirmMatchCommand>();
        CreateMap<CancelRideRequest, CancelRideCommand>();
        CreateMap<CancelMatchRequest, CancelMatchCommand>();

        #endregion

        #region Queries

        #endregion

        #endregion

        #region Passenger

        #region Commands

        CreateMap<SendMatchRequest, SendMatchCommand>();

        #endregion

        #region Queries

        CreateMap<FindRidesByRouteRequest, FindRidesByRouteQuery>();
        CreateMap<GetRecommendedPriceRequest, GetRecommendedPriceQuery>();
        CreateMap<GetRideInfoRequest, GetRideInfoQuery>();

        #endregion

        #endregion
    }
}