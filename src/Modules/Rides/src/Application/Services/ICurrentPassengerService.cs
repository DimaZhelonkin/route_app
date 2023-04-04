using Ark.Rides.Domain.Aggregates.Passenger;
using Ark.SharedLib.Application.Identity;

namespace Ark.Rides.Application.Services;

public interface ICurrentPassengerService : ICurrentUserService
{
    Task<Passenger?> GetPassenger(CancellationToken cancellationToken = default);
}