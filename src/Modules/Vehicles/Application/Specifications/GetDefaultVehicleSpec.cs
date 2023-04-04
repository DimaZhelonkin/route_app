using Ardalis.Specification;
using Ark.IdentityServer.Domain.ApplicationUser;
using Ark.Vehicles.Aggregates;

namespace Ark.Vehicles.Specifications;

public class GetDefaultVehicleSpec : Specification<Vehicle>
{
    public GetDefaultVehicleSpec(VehicleOwnerId ownerId)
    {
        Query
            .Where(v => v.Owner.Id == ownerId && v.IsDefault);
    }
    public GetDefaultVehicleSpec(IdentityId ownerIdentityId)
    {
        Query
            .Where(v => v.Owner.IdentityId == ownerIdentityId && v.IsDefault);
    }
}