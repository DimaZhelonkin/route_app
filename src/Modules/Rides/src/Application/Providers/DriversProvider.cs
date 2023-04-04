using Ark.IdentityServer.Domain.ApplicationUser;
using Ark.Rides.Application.Repositories;
using Ark.Rides.Application.Specifications.DriverSpecs;
using Ark.Rides.Domain.Aggregates.Driver;

namespace Ark.Rides.Application.Providers;

internal class DriversProvider : IDriversProvider
{
    private readonly IDriversRepository _driversRepository;

    public DriversProvider(IDriversRepository driversRepository)
    {
        _driversRepository = driversRepository;
    }

    #region IDriversProvider Members

    public async Task<Driver?> GetDriver(IdentityId id, CancellationToken cancellationToken = default)
    {
        var specification = new GetDriverByIdentityIdSpec(id);
        var driver = await _driversRepository.FirstOrDefaultAsync(specification, cancellationToken);
        return driver;
    }

    public Task<Driver?> GetDriver(DriverId id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    #endregion
}