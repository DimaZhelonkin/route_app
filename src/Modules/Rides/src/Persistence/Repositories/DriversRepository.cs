using Ardalis.Specification;
using Ark.Rides.Application.Repositories;
using Ark.Rides.Domain.Aggregates.Driver;
using Ark.SharedLib.Persistence.Repositories;

namespace Ark.Rides.Persistence.Repositories;

public class DriversRepository : EFRepository<Driver, DriverId, RidesDbContext>, IDriversRepository
{
    public DriversRepository(RidesDbContext context, ISpecificationEvaluator? specificationEvaluator = null) : base(
        context, specificationEvaluator)
    {
    }
}