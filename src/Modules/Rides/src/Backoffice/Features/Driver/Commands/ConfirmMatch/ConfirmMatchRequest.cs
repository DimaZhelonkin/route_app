using Ark.SharedLib.Common.CQS.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace Ark.Rides.Backoffice.Features.Driver.Commands.ConfirmMatch;

public record ConfirmMatchRequest : CommandResult
{
    [FromRoute]
    public Guid RideRequestId { get; set; }
}