using Ark.Rides.Domain.Aggregates.Driver;
using Ark.Vehicles.Aggregates;

namespace Ark.Rides.Application.Contracts;

public interface IDriverVehiclesService
{
    Task<Vehicle?> GetDefaultVehicle(DriverId driverId, CancellationToken cancellationToken = default);
}