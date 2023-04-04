using Ark.SharedLib.Common.CQS.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace Ark.Rides.Backoffice.Features.Driver.Commands.CancelRide;

public record CancelRideRequest : CommandResult
{
    [FromRoute]
    public Guid RideId { get; set; }
}