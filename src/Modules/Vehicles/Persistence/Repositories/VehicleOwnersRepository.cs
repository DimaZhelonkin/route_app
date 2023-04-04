using Ardalis.Specification;
using Ark.SharedLib.Persistence.Repositories;
using Ark.Vehicles.Aggregates;

namespace Ark.Vehicles.Repositories;

public class VehicleOwnersRepository : EFRepository<VehicleOwner, VehicleOwnerId, VehiclesDbContext>, IVehicleOwnersRepository
{
    public VehicleOwnersRepository(VehiclesDbContext context, ISpecificationEvaluator? specificationEvaluator = null) :
        base(
            context, specificationEvaluator)
    {
    }
}