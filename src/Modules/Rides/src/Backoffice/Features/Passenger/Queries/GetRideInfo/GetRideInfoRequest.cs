using Ark.Rides.Application.Features.Passenger.Queries.GetRideInfo;
using Ark.SharedLib.Common.CQS.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace Ark.Rides.Backoffice.Features.Passenger.Queries.GetRideInfo;

/// <summary>
///     Получить информацию для карточки поездки
/// </summary>
public class GetRideInfoRequest : QueryResult<GetRideInfoResponseData>
{
    /// <summary>
    ///     Ride identifier
    /// </summary>
    [FromRoute]
    public Guid RideId { get; set; }
}