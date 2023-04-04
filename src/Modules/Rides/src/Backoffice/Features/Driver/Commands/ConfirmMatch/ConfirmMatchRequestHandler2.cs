using Ark.Rides.Application.Features.Driver.Commands.ConfirmMatch;
using Ark.SharedLib.Common.CQS.Implementations;
using Ark.SharedLib.Common.Results;
using AutoMapper;
using MediatR;

namespace Ark.Rides.Backoffice.Features.Driver.Commands.ConfirmMatch;

public class ConfirmMatchRequestHandler : CommandHandler<ConfirmMatchRequest>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public ConfirmMatchRequestHandler(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    public override async Task<Result> Handle(ConfirmMatchRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<ConfirmMatchCommand>(request);
        var result = await _mediator.Send(command, cancellationToken);
        return result;
    }
}