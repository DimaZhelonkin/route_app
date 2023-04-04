using Ark.Rides.Application.Features.Driver.Queries.GetListOfPassengers;
using Ark.Rides.Domain.Aggregates.RideRequest;
using Ark.SharedLib.Common.CQS.Implementations;
using Ark.SharedLib.Common.Results;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ark.Rides.Backoffice.Features.Driver.Queries;

/// <summary>
/// </summary>
public class GetRideRequestsRequest : QueryResult<GetRideRequestsResultData>
{
    /// <summary>
    ///     Какие запросы получить, одобренные \ не одобренные \ все (null)
    /// </summary>
    [FromQuery]
    public RideRequestStatus? Type { get; set; }
}

public class GetRideRequestsValidator : AbstractValidator<GetRideRequestsRequest>
{
}

/// <summary>
/// </summary>
public class GetRideRequestsRequestHandler : QueryHandler<GetRideRequestsRequest,
    GetRideRequestsResultData>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public GetRideRequestsRequestHandler(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    public override async Task<Result<GetRideRequestsResultData>> Handle(GetRideRequestsRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<GetRideRequestsQuery>(request);
        var result = await _mediator.Send(command, cancellationToken);
        return result;
    }
}