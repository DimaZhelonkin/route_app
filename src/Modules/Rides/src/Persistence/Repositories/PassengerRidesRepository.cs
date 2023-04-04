using Ardalis.Specification;
using Ark.Rides.Application.Repositories;
using Ark.Rides.Domain.Aggregates.PassengerRide;
using Ark.SharedLib.Persistence.Repositories;

namespace Ark.Rides.Persistence.Repositories;

public class PassengerRidesRepository : EFRepository<PassengerRide, PassengerRideId, RidesDbContext>, IPassengerRidesRepository
{
    public PassengerRidesRepository(RidesDbContext context, ISpecificationEvaluator? specificationEvaluator = null) :
        base(
            context, specificationEvaluator)
    {
    }
}