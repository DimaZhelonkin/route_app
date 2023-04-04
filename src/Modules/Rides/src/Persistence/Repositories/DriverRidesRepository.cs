using Ardalis.Specification;
using Ark.Rides.Application.Repositories;
using Ark.Rides.Domain.Aggregates.DriverRide;
using Ark.SharedLib.Persistence.Repositories;

namespace Ark.Rides.Persistence.Repositories;

public class DriverRidesRepository : EFRepository<DriverRide, DriverRideId, RidesDbContext>, IDriverRidesRepository
{
    public DriverRidesRepository(RidesDbContext context, ISpecificationEvaluator? specificationEvaluator = null) : base(
        context, specificationEvaluator)
    {
    }
}