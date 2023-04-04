using Ark.SharedLib.Common.CQS.Implementations;

namespace Ark.Rides.Application.Features.Driver.Commands.RejectMatch;

public record RejectMatchCommand : CommandResult
{
    public Guid RideRequestId { get; set; }
}