using Ark.Rides.Application.Features.Driver.Commands.RejectMatch;
using Ark.SharedLib.Common.CQS.Implementations;
using Ark.SharedLib.Common.Results;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ark.Rides.Backoffice.Features.Driver.Commands.RejectMatch;

/// <summary>
/// </summary>
public record RejectMatchRequest : CommandResult
{
    [FromRoute]
    public Guid RideRequestId { get; set; }
}

/// <summary>
/// </summary>
public class RejectMatchRequestHandler : CommandHandler<RejectMatchRequest>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public RejectMatchRequestHandler(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    public override async Task<Result> Handle(RejectMatchRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<RejectMatchCommand>(request);
        var result = await _mediator.Send(command, cancellationToken);
        return result;
    }
}

/// <summary>
/// </summary>
public class RejectMatchValidator : AbstractValidator<RejectMatchRequest>
{
}