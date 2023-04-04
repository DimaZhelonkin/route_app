using Ark.Rides.Domain.Aggregates.PassengerRide;
using Ark.SharedLib.Application.Abstractions.Repositories;

namespace Ark.Rides.Application.Repositories;

public interface IPassengerRidesRepository : IEntityRepository<PassengerRide, PassengerRideId>
{
}