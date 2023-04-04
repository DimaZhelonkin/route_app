using Ark.SharedLib.Common.CQS.Implementations;

namespace Ark.Rides.Application.Features.Passenger.Commands.CancelMatch;

public record CancelMatchCommand : CommandResult
{
    public Guid RideRequestId { get; set; }
}