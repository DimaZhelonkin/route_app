using Ark.IdentityServer.Domain.ApplicationUser;
using Ark.Vehicles.Aggregates;
using Ark.Vehicles.Contracts;
using Ark.Vehicles.Repositories;
using Ark.Vehicles.Specifications;

namespace Ark.Vehicles.Services;

internal class VehiclesProviderService : IVehiclesProviderService
{
    private readonly IVehiclesRepository _vehiclesRepository;

    public VehiclesProviderService(IVehiclesRepository vehiclesRepository)
    {
        _vehiclesRepository = vehiclesRepository;
    }

    #region IVehiclesProviderService Members

    public Task<Vehicle?> GetById(VehicleId id, CancellationToken cancellationToken = default)
    {
        var spec = new GetVehicleSpec(id);
        return _vehiclesRepository.FirstOrDefaultAsync(spec, cancellationToken);
    }

    public Task<Vehicle?> GetById(VehicleId id, VehicleOwnerId ownerId, CancellationToken cancellationToken = default)
    {
        var spec = new GetVehicleSpec(id, ownerId);
        return _vehiclesRepository.FirstOrDefaultAsync(spec, cancellationToken);
    }

    public async Task<Vehicle?> GetDefault(VehicleOwnerId ownerId, CancellationToken cancellationToken = default)
    {
        var specification = new GetDefaultVehicleSpec(ownerId);
        var vehicle = await _vehiclesRepository.FirstOrDefaultAsync(specification, cancellationToken);

        return vehicle;
    }

    public async Task<Vehicle?> GetDefault(IdentityId ownerIdentityId, CancellationToken cancellationToken = default)
    {
        var specification = new GetDefaultVehicleSpec(ownerIdentityId);
        var vehicle = await _vehiclesRepository.FirstOrDefaultAsync(specification, cancellationToken);

        return vehicle;
    }

    public async Task<Vehicle?> GetByIdOrDefault(VehicleId? id, VehicleOwnerId ownerId,
        CancellationToken cancellationToken)
    {
        Vehicle? vehicle;
        if (id is null)
            vehicle = await GetDefault(ownerId, cancellationToken);
        else
            vehicle = await GetById(id, ownerId, cancellationToken);

        return vehicle;
    }

    public async Task<Vehicle?> GetByIdOrDefault(VehicleId? id, IdentityId ownerIdentityId,
        CancellationToken cancellationToken)
    {
        Vehicle? vehicle;
        if (id is null)
            vehicle = await GetDefault(ownerIdentityId, cancellationToken);
        else
            vehicle = await GetById(id, ownerIdentityId, cancellationToken);

        return vehicle;
    }

    public Task<Vehicle?> GetById(VehicleId id, IdentityId ownerIdentityId,
        CancellationToken cancellationToken = default)
    {
        var spec = new GetVehicleSpec(id, ownerIdentityId);
        return _vehiclesRepository.FirstOrDefaultAsync(spec, cancellationToken);
    }

    #endregion
}