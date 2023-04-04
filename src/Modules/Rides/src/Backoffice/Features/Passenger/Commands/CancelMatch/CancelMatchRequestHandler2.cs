using Ark.Rides.Application.Features.Passenger.Commands.CancelMatch;
using Ark.SharedLib.Common.CQS.Implementations;
using Ark.SharedLib.Common.Results;
using AutoMapper;
using MediatR;

namespace Ark.Rides.Backoffice.Features.Passenger.Commands.CancelMatch;

/// <summary>
/// </summary>
public class CancelMatchRequestHandler : CommandHandler<CancelMatchRequest>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public CancelMatchRequestHandler(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    public override async Task<Result> Handle(CancelMatchRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<CancelMatchCommand>(request);
        var result = await _mediator.Send(command, cancellationToken);
        return result;
    }
}