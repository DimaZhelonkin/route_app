using Ark.Rides.Application.Features.Common.Commands.StartRide;
using Ark.SharedLib.Common.CQS.Implementations;
using Ark.SharedLib.Common.Results;
using AutoMapper;
using MediatR;

namespace Ark.Rides.Backoffice.Features.Driver.Commands.StartRide;

/// <summary>
/// </summary>
public class StartRideRequestHandler : CommandHandler<StartRideRequest>
{
    private readonly IMapper _mapper;
    private readonly ISender _sender;

    public StartRideRequestHandler(IMapper mapper, ISender sender)
    {
        _mapper = mapper;
        _sender = sender;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<Result> Handle(StartRideRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<StartRideCommand>(request);
        var result = await _sender.Send(command, cancellationToken);
        return result;
    }
}