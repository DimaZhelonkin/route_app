using Ardalis.Specification;
using Ark.SharedLib.Persistence.Repositories;
using Ark.Vehicles.Aggregates;

namespace Ark.Vehicles.Repositories;

public class VehiclesRepository : EFRepository<Vehicle, VehicleId, VehiclesDbContext>, IVehiclesRepository
{
    public VehiclesRepository(VehiclesDbContext context, ISpecificationEvaluator? specificationEvaluator = null) : base(
        context, specificationEvaluator)
    {
    }
}