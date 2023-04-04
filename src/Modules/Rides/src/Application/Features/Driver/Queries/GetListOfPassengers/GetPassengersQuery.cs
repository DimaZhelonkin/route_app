using Ark.Rides.Application.Repositories;
using Ark.Rides.Domain.Aggregates.RideRequest;
using Ark.SharedLib.Common.CQS.Implementations;
using Ark.SharedLib.Common.Results;

namespace Ark.Rides.Application.Features.Driver.Queries.GetListOfPassengers;

public class GetRideRequestsQuery : QueryResult<GetRideRequestsResultData>
{
    /// <summary>
    ///     Какие запросы получить, одобренные \ не одобренные \ все (null)
    /// </summary>
    public RideRequestStatus? Type { get; set; }
}

public class GetRideRequestsQueryHandler : QueryHandler<GetRideRequestsQuery, GetRideRequestsResultData>
{
    private readonly IRideRequestsRepository _rideRequestsRepository;

    public GetRideRequestsQueryHandler(IRideRequestsRepository rideRequestsRepository)
    {
        _rideRequestsRepository = rideRequestsRepository;
    }

    public override async Task<Result<GetRideRequestsResultData>> Handle(GetRideRequestsQuery query,
        CancellationToken cancellationToken = default)
    {
        var rideRequests = await _rideRequestsRepository.ListAsync(cancellationToken);

        throw new NotImplementedException();
        var data = new GetRideRequestsResultData();
        return Result.Success(data);
    }
}

public class GetRideRequestsResultData
{
    public Guid RouteId { get; set; }
}