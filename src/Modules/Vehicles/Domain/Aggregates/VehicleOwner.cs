using Ark.IdentityServer.Domain.ApplicationUser;
using Ark.SharedLib.Domain.Models.Entities;
using Ark.StronglyTypedIds;

namespace Ark.Vehicles.Aggregates;

public partial record VehicleOwnerId(Guid Value) : StronglyTypedId<Guid>(Value);
public class VehicleOwner : AggregateRootBase<VehicleOwnerId>
{
    private VehicleOwner(VehicleOwnerId id, IdentityId identityId) : base(id)
    {
        IdentityId = identityId;
    }

    public IdentityId IdentityId { get; }

    public static VehicleOwner Create(VehicleOwnerId id, IdentityId identityId) => new(id, identityId);
}