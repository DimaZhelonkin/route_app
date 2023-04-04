using Ark.Rides.Application.Features.Passenger.Queries.GetRecommendedPrice;
using Ark.SharedLib.Common.CQS.Implementations;
using Ark.SharedLib.Common.Results;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ark.Rides.Backoffice.Features.Passenger.Queries.GetRecommendedPriceByRoute;

/// <summary>
/// </summary>
public class GetRecommendedPriceRequest : QueryResult<GetRecommendedPriceResultData>
{
    [FromRoute]
    public Guid RouteId { get; set; }
}

public class GetRecommendedPriceValidator : AbstractValidator<GetRecommendedPriceRequest>
{
}

/// <summary>
/// </summary>
public class GetRecommendedPriceRequestHandler : QueryHandler<GetRecommendedPriceRequest,
    GetRecommendedPriceResultData>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public GetRecommendedPriceRequestHandler(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    public override async Task<Result<GetRecommendedPriceResultData>> Handle(GetRecommendedPriceRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<GetRecommendedPriceQuery>(request);
        var result = await _mediator.Send(command, cancellationToken);
        return result;
    }
}