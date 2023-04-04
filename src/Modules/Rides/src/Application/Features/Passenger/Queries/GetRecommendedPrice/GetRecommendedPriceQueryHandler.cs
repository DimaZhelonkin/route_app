using Ark.Rides.Application.Contracts;
using Ark.SharedLib.Common.CQS.Implementations;
using Ark.SharedLib.Common.Results;

namespace Ark.Rides.Application.Features.Passenger.Queries.GetRecommendedPrice;

public class
    GetRecommendedPriceByRouteQueryHandler : QueryHandler<GetRecommendedPriceQuery, GetRecommendedPriceResultData>
{
    private readonly IRidesService _ridesService;

    public GetRecommendedPriceByRouteQueryHandler(IRidesService ridesService)
    {
        _ridesService = ridesService;
    }

    public override async Task<Result<GetRecommendedPriceResultData>> Handle(GetRecommendedPriceQuery query,
        CancellationToken cancellationToken = default)
    {
        var recommendedPrice = await _ridesService.GetRecommendedPrice(query.RouteId);
        throw new NotImplementedException();
        var data = new GetRecommendedPriceResultData();
        return Result.Success(data);
    }
}