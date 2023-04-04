using Ark.Rides.Application.Features.Common.Commands.CancelRide;
using Ark.SharedLib.Common.CQS.Implementations;
using Ark.SharedLib.Common.Results;
using AutoMapper;
using MediatR;

namespace Ark.Rides.Backoffice.Features.Driver.Commands.CancelRide;

/// <summary>
/// </summary>
public class CancelRideRequestHandler : CommandHandler<CancelRideRequest>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public CancelRideRequestHandler(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    public override async Task<Result> Handle(CancelRideRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<CancelRideCommand>(request);
        var result = await _mediator.Send(command, cancellationToken);
        return result;
    }
}