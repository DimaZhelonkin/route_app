using Ark.SharedLib.Common.CQS.Implementations;
using Ark.SharedLib.Common.Results;

namespace Ark.Rides.Application.Features.Passenger.Queries.GetRideInfo;

public class GetRideInfoQueryHandler : QueryHandler<GetRideInfoQuery, GetRideInfoResponseData>
{
    public override async Task<Result<GetRideInfoResponseData>> Handle(GetRideInfoQuery query,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
        var data = new GetRideInfoResponseData();
        return Result.Success(data);
    }
}