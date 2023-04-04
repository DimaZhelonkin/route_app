using Ark.Rides.Domain.Aggregates.Passenger;
using Ark.SharedLib.Application.Abstractions.Repositories;

namespace Ark.Rides.Application.Repositories;

public interface IPassengersRepository : IEntityRepository<Passenger, PassengerId>
{
}