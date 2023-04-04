using Ark.SharedLib.Common.CQS.Implementations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Ark.Rides.Backoffice.Features.Driver.Commands.StartRide;

/// <summary>
/// </summary>
public record StartRideRequest : CommandResult
{
    [FromRoute]
    public Guid RideId { get; set; }
}

/// <summary>
/// </summary>
public class StartRideValidator : AbstractValidator<StartRideRequest>
{
}