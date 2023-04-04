using Ark.IdentityServer.Domain.ApplicationUser;
using Ark.Vehicles.Aggregates;

namespace Ark.Vehicles.Contracts;

public interface IVehiclesProviderService
{
    Task<Vehicle?> GetById(VehicleId id, CancellationToken cancellationToken = default);
    Task<Vehicle?> GetById(VehicleId id, VehicleOwnerId ownerId, CancellationToken cancellationToken = default);
    Task<Vehicle?> GetById(VehicleId id, IdentityId ownerIdentityId, CancellationToken cancellationToken = default);
    Task<Vehicle?> GetDefault(VehicleOwnerId ownerId, CancellationToken cancellationToken = default);
    Task<Vehicle?> GetDefault(IdentityId ownerIdentityId, CancellationToken cancellationToken = default);
    Task<Vehicle?> GetByIdOrDefault(VehicleId? id, VehicleOwnerId ownerId, CancellationToken cancellationToken = default);
    Task<Vehicle?> GetByIdOrDefault(VehicleId? id, IdentityId ownerIdentityId, CancellationToken cancellationToken = default);
}