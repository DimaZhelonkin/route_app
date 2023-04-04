using Ardalis.Specification;
using Ark.Rides.Domain.Aggregates.RideRequest;

namespace Ark.Rides.Application.Specifications.RideRequestSpecs;

public sealed class GetRideRequestByIdSpec : Specification<RideRequest>
{
    public GetRideRequestByIdSpec(RideRequestId id)
    {
        Query
            .Where(x => x.Id == id);
    }
}