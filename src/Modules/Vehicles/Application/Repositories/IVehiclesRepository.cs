using Ark.SharedLib.Application.Abstractions.Repositories;
using Ark.Vehicles.Aggregates;

namespace Ark.Vehicles.Repositories;

public interface IVehiclesRepository : IEntityRepository<Vehicle, VehicleId>
{
}