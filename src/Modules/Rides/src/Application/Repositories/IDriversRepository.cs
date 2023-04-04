using Ark.Rides.Domain.Aggregates.Driver;
using Ark.SharedLib.Application.Abstractions.Repositories;

namespace Ark.Rides.Application.Repositories;

public interface IDriversRepository : IEntityRepository<Driver, DriverId>
{
}