using Ark.SharedLib.Common.CQS.Implementations;

namespace Ark.Rides.Application.Features.Common.Commands.StartRide;

public record StartRideCommand : CommandResult
{
    public Guid RideId { get; set; }
}