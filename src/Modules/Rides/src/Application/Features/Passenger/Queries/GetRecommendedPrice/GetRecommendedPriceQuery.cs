using Ark.SharedLib.Common.CQS.Implementations;

namespace Ark.Rides.Application.Features.Passenger.Queries.GetRecommendedPrice;

public class GetRecommendedPriceQuery : QueryResult<GetRecommendedPriceResultData>
{
    public Guid RouteId { get; set; }
}