using Ark.Rides.Application.Repositories;
using Ark.Rides.Application.Specifications.RideRequestSpecs;
using Ark.SharedLib.Common.CQS.Implementations;
using Ark.SharedLib.Common.Results;

namespace Ark.Rides.Application.Features.Driver.Commands.ConfirmMatch;

public class ConfirmMatchCommandHandler : CommandHandler<ConfirmMatchCommand>
{
    private readonly IRideRequestsRepository _rideRequestsRepository;

    public ConfirmMatchCommandHandler(IRideRequestsRepository rideRequestsRepository)
    {
        _rideRequestsRepository = rideRequestsRepository;
    }

    public override async Task<Result> Handle(ConfirmMatchCommand command,
        CancellationToken cancellationToken = default)
    {
        var getByIdSpec = new GetRideRequestByIdSpec(command.RideRequestId);
        var rideRequest = await _rideRequestsRepository.FirstOrDefaultAsync(getByIdSpec, cancellationToken);
        if (rideRequest is null)
            return Result.Error("ConfirmMatchCommand", "Ride request not found");

        rideRequest.Confirm();
        return Result.Success();
    }
}