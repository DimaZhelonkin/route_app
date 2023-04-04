using Ark.Rides.Application.Features.Common.Commands.FinishRide;
using Ark.SharedLib.Common.CQS.Implementations;
using Ark.SharedLib.Common.Results;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ark.Rides.Backoffice.Features.Driver.Commands.FinishRide;

/// <summary>
/// </summary>
public record FinishRideRequest : CommandResult
{
    /// <summary>
    ///     Driver ride id
    /// </summary>
    [FromRoute]
    public Guid RideId { get; set; }
}

/// <summary>
/// </summary>
public class FinishRideRequestHandler : CommandHandler<FinishRideRequest>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public FinishRideRequestHandler(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    public override async Task<Result> Handle(FinishRideRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<FinishRideCommand>(request);
        var result = await _mediator.Send(command, cancellationToken);
        return result;
    }
}

/// <summary>
/// </summary>
public class FinishRideValidator : AbstractValidator<FinishRideRequest>
{
}