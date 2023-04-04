using Ardalis.Specification;
using Ark.IdentityServer.Domain.ApplicationUser;
using Ark.Vehicles.Aggregates;

namespace Ark.Vehicles.Specifications;

public class GetVehicleSpec : Specification<Vehicle>
{
    public GetVehicleSpec(VehicleId id)
    {
        Query
            .Where(v => v.Id == id);
    }

    public GetVehicleSpec(VehicleId id, VehicleOwnerId ownerId) : this(id)
    {
        Query
            .Where(v => v.Owner.Id == ownerId);
    }
    public GetVehicleSpec(VehicleId id, IdentityId ownerIdentityId) : this(id)
    {
        Query
            .Where(v => v.Owner.IdentityId == ownerIdentityId);
    }
}