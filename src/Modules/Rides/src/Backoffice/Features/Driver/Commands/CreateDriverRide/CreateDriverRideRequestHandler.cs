using Ark.Rides.Application.Features.Driver.Commands.CreateDriverRide;
using Ark.SharedLib.Common.CQS.Implementations;
using Ark.SharedLib.Common.Results;
using AutoMapper;
using MediatR;

namespace Ark.Rides.Backoffice.Features.Driver.Commands.CreateDriverRide;

public class CreateDriverRideRequestHandler : CommandHandler<CreateDriverRideRequest>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public CreateDriverRideRequestHandler(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    public override async Task<Result> Handle(CreateDriverRideRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<CreateDriverRideCommand>(request);
        var result = await _mediator.Send(command, cancellationToken);
        return result;
    }
}