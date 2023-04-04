using Ark.Rides.Application.Contracts;
using Ark.Rides.Application.Providers;
using Ark.Rides.Domain.Aggregates.Driver;
using Ark.Vehicles.Aggregates;
using Ark.Vehicles.Repositories;
using Ark.Vehicles.Specifications;

namespace Ark.Rides.Application.Services;

internal class DriverVehiclesService : IDriverVehiclesService
{
    private readonly IVehiclesRepository _vehiclesRepository;
    private readonly IDriversProvider _driversProvider;

    public DriverVehiclesService(IVehiclesRepository vehiclesRepository, IDriversProvider driversProvider)
    {
        _vehiclesRepository = vehiclesRepository;
        _driversProvider = driversProvider;
    }

    #region IDriverVehiclesService Members

    public async Task<Vehicle?> GetDefaultVehicle(DriverId driverId, CancellationToken cancellationToken)
    {
        var driver = await _driversProvider.GetDriver(driverId, cancellationToken);
        if (driver is null)
            throw new Exception("Driver not found");// TODO to custom exception
        var specification = new GetDefaultVehicleSpec(driver.IdentityId);
        var vehicle = await _vehiclesRepository.FirstOrDefaultAsync(specification, cancellationToken);
        return vehicle;
    }

    #endregion
}