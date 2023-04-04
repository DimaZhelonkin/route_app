using Ardalis.Specification;
using Ark.Rides.Application.Repositories;
using Ark.Rides.Domain.Aggregates.RideRequest;
using Ark.SharedLib.Persistence.Repositories;

namespace Ark.Rides.Persistence.Repositories;

public class RideRequestsRepository : EFRepository<RideRequest, RideRequestId, RidesDbContext>, IRideRequestsRepository
{
    public RideRequestsRepository(RidesDbContext context, ISpecificationEvaluator? specificationEvaluator = null) :
        base(
            context, specificationEvaluator)
    {
    }
}