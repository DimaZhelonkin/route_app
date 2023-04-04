using Ark.SharedLib.Common.CQS.Implementations;
using Ark.SharedLib.Common.Results;

namespace Ark.Rides.Application.Features.Common.Commands.CancelRide;

public class CancelRideCommandHandler : CommandHandler<CancelRideCommand>
{
    public override Task<Result> Handle(CancelRideCommand command, CancellationToken cancellationToken = default) =>
        throw new NotImplementedException();
}