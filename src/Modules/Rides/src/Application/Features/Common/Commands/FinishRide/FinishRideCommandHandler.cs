using Ark.Rides.Application.Repositories;
using Ark.Rides.Application.Specifications.DriverRideSpecs;
using Ark.Rides.Domain.Aggregates.DriverRide;
using Ark.Rides.Domain.Aggregates.Ride;
using Ark.SharedLib.Common.CQS.Implementations;
using Ark.SharedLib.Common.Results;

namespace Ark.Rides.Application.Features.Common.Commands.FinishRide;

public class FinishRideCommandHandler : CommandHandler<FinishRideCommand>
{
    private readonly IDriverRidesRepository _driverRidesRepository;

    public FinishRideCommandHandler(IDriverRidesRepository driverRidesRepository)
    {
        _driverRidesRepository = driverRidesRepository;
    }

    public override async Task<Result> Handle(FinishRideCommand command, CancellationToken cancellationToken = default)
    {
        var driverRideId = new DriverRideId(command.RideId);
        var spec = new GetDriverRideByIdSpec(driverRideId);
        var driverRide = await _driverRidesRepository.FirstOrDefaultAsync(spec, cancellationToken);
        if (driverRide is null)
            return Result.Error("FinishRideCommand", "Driver ride not found");
        driverRide.Finish();

        return Result.Success();
    }
}