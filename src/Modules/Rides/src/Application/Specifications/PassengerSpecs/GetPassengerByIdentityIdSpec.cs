using Ardalis.Specification;
using Ark.IdentityServer.Domain.ApplicationUser;
using Ark.Rides.Domain.Aggregates.Passenger;

namespace Ark.Rides.Application.Specifications.PassengerSpecs;

public class GetPassengerByIdentityIdSpec : Specification<Passenger>
{
    public GetPassengerByIdentityIdSpec(IdentityId identityId)
    {
        Query
            .Where(x => x.IdentityId == identityId);
    }
}