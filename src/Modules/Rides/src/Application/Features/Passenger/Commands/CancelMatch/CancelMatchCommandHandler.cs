using Ark.Rides.Application.Repositories;
using Ark.Rides.Application.Specifications.RideRequestSpecs;
using Ark.Rides.Domain.Aggregates.RideRequest;
using Ark.SharedLib.Common.CQS.Implementations;
using Ark.SharedLib.Common.Results;

namespace Ark.Rides.Application.Features.Passenger.Commands.CancelMatch;

public class CancelMatchCommandHandler : CommandHandler<CancelMatchCommand>
{
    private readonly IRideRequestsRepository _rideRequestsRepository;

    public CancelMatchCommandHandler(IRideRequestsRepository rideRequestsRepository)
    {
        _rideRequestsRepository = rideRequestsRepository;
    }

    public override async Task<Result> Handle(CancelMatchCommand command,
        CancellationToken cancellationToken = default)
    {
        var getByIdSpec = new GetRideRequestByIdSpec(new RideRequestId(command.RideRequestId));
        var rideRequest = await _rideRequestsRepository.FirstOrDefaultAsync(getByIdSpec, cancellationToken);
        if (rideRequest is null)
            return Result.Error("CancelMatchCommand", "Ride request not found");

        rideRequest.Cancel();
        return Result.Success();
    }
}