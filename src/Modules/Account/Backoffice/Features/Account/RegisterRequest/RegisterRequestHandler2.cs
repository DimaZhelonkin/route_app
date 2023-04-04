using Ark.SharedLib.Common.CQS.Implementations;
using Ark.SharedLib.Common.Results;
using AutoMapper;
using MediatR;

namespace Ark.Account.Features.Account.RegisterRequest;

public class RegisterRequestHandler : CommandHandler<RegisterRequest>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public RegisterRequestHandler(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    public override Task<Result> Handle(RegisterRequest request, CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<RegisterCommand.RegisterCommand>(request);
        return _mediator.Send(command, cancellationToken);
    }
}