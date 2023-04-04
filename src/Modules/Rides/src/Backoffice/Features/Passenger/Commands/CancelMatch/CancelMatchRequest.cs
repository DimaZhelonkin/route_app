using Ark.SharedLib.Common.CQS.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace Ark.Rides.Backoffice.Features.Passenger.Commands.CancelMatch;

public record CancelMatchRequest : CommandResult
{
    [FromRoute]
    public Guid RideRequestId { get; set; }
}