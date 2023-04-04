using Ark.SharedLib.Common.CQS.Implementations;

namespace Ark.Rides.Application.Features.Common.Commands.CancelRide;

public record CancelRideCommand : CommandResult
{
    public Guid RideId { get; set; }
}