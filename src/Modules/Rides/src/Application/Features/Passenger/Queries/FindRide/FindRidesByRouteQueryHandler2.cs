using Ark.Rides.Application.Models;
using Ark.SharedLib.Common.CQS.Implementations;
using Ark.SharedLib.Common.Paging;
using Ark.SharedLib.Common.Results;
using AutoMapper;

namespace Ark.Rides.Application.Features.Passenger.Queries.FindRide;

public class FindRidesByRouteQueryHandler : QueryHandler<FindRidesByRouteQuery, FindRidesByRouteData>
{
    private readonly IMapper _mapper;

    public FindRidesByRouteQueryHandler(IMapper mapper)
    {
        _mapper = mapper;
    }

    public override async Task<Result<FindRidesByRouteData>> Handle(FindRidesByRouteQuery command,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
        var response = new FindRidesByRouteData();
        var pagedInfo = new PagedInfo(1, 2, 20);
        var rides = new List<RideInfo>(); // TODO get rides
        var route = "new LineString()"; // TODO get route
        var pagedRides = new PagedData<RideInfo>(rides, pagedInfo);
        response.Route = route;
        response.Rides = pagedRides;
        var result = Result.Success(response);
        return result;
    }
}