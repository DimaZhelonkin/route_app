using Ark.Rides.Application.Repositories;
using Ark.Rides.Application.Specifications.RideRequestSpecs;
using Ark.Rides.Domain.Aggregates.RideRequest;
using Ark.SharedLib.Common.CQS.Implementations;
using Ark.SharedLib.Common.Results;

namespace Ark.Rides.Application.Features.Driver.Commands.RejectMatch;

public class RejectMatchCommandHandler : CommandHandler<RejectMatchCommand>
{
    private readonly IRideRequestsRepository _rideRequestsRepository;

    public RejectMatchCommandHandler(IRideRequestsRepository rideRequestsRepository)
    {
        _rideRequestsRepository = rideRequestsRepository;
    }

    public override async Task<Result> Handle(RejectMatchCommand command,
        CancellationToken cancellationToken = default)
    {
        var getByIdSpec = new GetRideRequestByIdSpec(new RideRequestId(command.RideRequestId));
        var rideRequest = await _rideRequestsRepository.FirstOrDefaultAsync(getByIdSpec, cancellationToken);
        if (rideRequest is null)
            return Result.Error("RejectMatchCommand", "Ride request not found");

        rideRequest.Reject();
        return Result.Success();
    }
}