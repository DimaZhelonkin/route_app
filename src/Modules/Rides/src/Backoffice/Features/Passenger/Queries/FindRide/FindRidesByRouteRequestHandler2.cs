using Ark.Rides.Application.Features.Passenger.Queries.FindRide;
using Ark.SharedLib.Common.CQS.Implementations;
using Ark.SharedLib.Common.Results;
using AutoMapper;
using MediatR;

namespace Ark.Rides.Backoffice.Features.Passenger.Queries.FindRide;

/// <summary>
/// </summary>
public class FindRidesByRouteRequestHandler : QueryHandler<FindRidesByRouteRequest, FindRidesByRouteData>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public FindRidesByRouteRequestHandler(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    public override async Task<Result<FindRidesByRouteData>> Handle(FindRidesByRouteRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<FindRidesByRouteQuery>(request);
        var result = await _mediator.Send(command, cancellationToken);
        return result;
    }
}