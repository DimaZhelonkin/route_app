using Ark.Rides.Application.Features.Passenger.Commands.SendFeedback;
using Ark.SharedLib.Common.CQS.Implementations;
using Ark.SharedLib.Common.Results;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ark.Rides.Backoffice.Features.Driver.Commands.SendFeedback;

/// <summary>
/// </summary>
public record SendFeedbackRequest : CommandResult
{
    [FromRoute]
    public Guid RideId { get; set; }

    [FromRoute]
    public Guid UserId { get; set; }

    public uint StarsCount { get; set; }
    public string FeedbackMessage { get; set; }
}

/// <summary>
/// </summary>
public class SendFeedbackRequestHandler : CommandHandler<SendFeedbackRequest>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public SendFeedbackRequestHandler(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    public override async Task<Result> Handle(SendFeedbackRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<SendFeedbackCommand>(request);
        var result = await _mediator.Send(command, cancellationToken);
        return result;
    }
}

/// <summary>
/// </summary>
public class SendFeedbackValidator : AbstractValidator<SendFeedbackRequest>
{
}