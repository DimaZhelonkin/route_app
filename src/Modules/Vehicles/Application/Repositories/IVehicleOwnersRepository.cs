using Ark.SharedLib.Application.Abstractions.Repositories;
using Ark.Vehicles.Aggregates;

namespace Ark.Vehicles.Repositories;

public interface IVehicleOwnersRepository : IEntityRepository<VehicleOwner, VehicleOwnerId>
{
}