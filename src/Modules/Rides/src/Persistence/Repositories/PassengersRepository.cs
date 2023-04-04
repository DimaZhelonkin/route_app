using Ardalis.Specification;
using Ark.Rides.Application.Repositories;
using Ark.Rides.Domain.Aggregates.Passenger;
using Ark.SharedLib.Persistence.Repositories;

namespace Ark.Rides.Persistence.Repositories;

public class PassengersRepository : EFRepository<Passenger, PassengerId, RidesDbContext>, IPassengersRepository
{
    public PassengersRepository(RidesDbContext context, ISpecificationEvaluator? specificationEvaluator = null) : base(
        context, specificationEvaluator)
    {
    }
}