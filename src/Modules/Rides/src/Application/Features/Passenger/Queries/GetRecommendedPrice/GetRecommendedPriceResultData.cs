namespace Ark.Rides.Application.Features.Passenger.Queries.GetRecommendedPrice;

public class GetRecommendedPriceResultData
{
    public Guid RouteId { get; set; }
    public decimal RecommendedPrice { get; set; }
}