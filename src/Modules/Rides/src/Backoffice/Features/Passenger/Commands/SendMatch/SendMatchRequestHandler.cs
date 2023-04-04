using Ark.Rides.Application.Features.Passenger.Commands.SendMatch;
using Ark.SharedLib.Common.CQS.Implementations;
using Ark.SharedLib.Common.Results;
using AutoMapper;
using MediatR;

namespace Ark.Rides.Backoffice.Features.Passenger.Commands.SendMatch;

/// <summary>
/// </summary>
public class SendMatchRequestHandler : CommandHandler<SendMatchRequest>
{
    private readonly IMapper _mapper;
    private readonly ISender _sender;

    public SendMatchRequestHandler(IMapper mapper, ISender sender)
    {
        _mapper = mapper;
        _sender = sender;
    }

    public override async Task<Result> Handle(SendMatchRequest request,
        CancellationToken cancellationToken = default)
    {
        var matchCommand = _mapper.Map<SendMatchCommand>(request);
        var result = await _sender.Send(matchCommand, cancellationToken);
        return result;
    }
}