namespace Ark.Rides.Application.Contracts;

public interface IRidesService
{
    Task<decimal> GetRecommendedPrice(Guid routeId);
}