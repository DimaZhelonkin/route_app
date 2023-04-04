using Ark.Rides.Domain.Aggregates.Driver;
using Ark.SharedLib.Application.Identity;

namespace Ark.Rides.Application.Services;

public interface ICurrentDriverService : ICurrentUserService
{
    Task<Driver?> GetDriver(CancellationToken cancellationToken = default);
}