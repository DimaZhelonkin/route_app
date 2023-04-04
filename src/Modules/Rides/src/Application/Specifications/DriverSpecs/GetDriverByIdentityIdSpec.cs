using Ardalis.Specification;
using Ark.IdentityServer.Domain.ApplicationUser;
using Ark.Rides.Domain.Aggregates.Driver;

namespace Ark.Rides.Application.Specifications.DriverSpecs;

public class GetDriverByIdentityIdSpec : Specification<Driver>
{
    public GetDriverByIdentityIdSpec(IdentityId identityId)
    {
        Query
            .Where(x => x.IdentityId == identityId);
    }
}