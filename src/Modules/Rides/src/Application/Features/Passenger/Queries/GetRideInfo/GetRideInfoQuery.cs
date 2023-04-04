using Ark.SharedLib.Common.CQS.Implementations;

namespace Ark.Rides.Application.Features.Passenger.Queries.GetRideInfo;

/// <summary>
/// </summary>
public class GetRideInfoQuery : QueryResult<GetRideInfoResponseData>
{
    public Guid RideId { get; set; }
}