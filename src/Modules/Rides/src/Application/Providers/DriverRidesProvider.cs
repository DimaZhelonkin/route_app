using Ark.Rides.Application.Repositories;
using Ark.Rides.Application.Specifications.DriverRideSpecs;
using Ark.Rides.Domain.Aggregates.DriverRide;

namespace Ark.Rides.Application.Providers;

internal class DriverRidesProvider : IDriverRidesProvider
{
    private readonly IDriverRidesRepository _driverRidesRepository;

    public DriverRidesProvider(IDriverRidesRepository driverRidesRepository)
    {
        _driverRidesRepository = driverRidesRepository;
    }

    #region IDriverRidesProvider Members

    public Task<DriverRide?> GetById(DriverRideId id, CancellationToken cancellationToken = default)
    {
        var specification = new GetDriverRideByIdSpec(id);
        return _driverRidesRepository.FirstOrDefaultAsync(specification, cancellationToken);
    }

    #endregion
}