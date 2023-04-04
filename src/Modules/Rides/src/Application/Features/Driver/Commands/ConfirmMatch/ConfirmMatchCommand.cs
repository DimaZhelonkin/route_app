using Ark.Rides.Domain.Aggregates.RideRequest;
using Ark.SharedLib.Common.CQS.Implementations;

namespace Ark.Rides.Application.Features.Driver.Commands.ConfirmMatch;

public record ConfirmMatchCommand : CommandResult
{
    public RideRequestId RideRequestId { get; set; }
}