using Ark.SharedLib.Common.CQS.Implementations;

namespace Ark.Rides.Application.Features.Common.Commands.FinishRide;

public record FinishRideCommand : CommandResult
{
    public Guid RideId { get; set; }
}