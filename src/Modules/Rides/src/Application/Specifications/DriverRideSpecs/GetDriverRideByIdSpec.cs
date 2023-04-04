using Ardalis.Specification;
using Ark.Rides.Domain.Aggregates.DriverRide;

namespace Ark.Rides.Application.Specifications.DriverRideSpecs;

public sealed class GetDriverRideByIdSpec : Specification<DriverRide>
{
    public GetDriverRideByIdSpec(DriverRideId id)
    {
        Query
            .Where(x => x.Id == id);
    }
}