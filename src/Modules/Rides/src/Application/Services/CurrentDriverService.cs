using System.Security.Claims;
using Ark.IdentityServer.Domain.ApplicationUser;
using Ark.Rides.Application.Providers;
using Ark.Rides.Domain.Aggregates.Driver;
using Ark.SharedLib.Application.Identity;

namespace Ark.Rides.Application.Services;

public class CurrentDriverService : ICurrentDriverService
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IDriversProvider _driversProvider;

    public CurrentDriverService(ICurrentUserService currentUserService, IDriversProvider driversProvider)
    {
        _currentUserService = currentUserService;
        _driversProvider = driversProvider;
    }

    #region ICurrentDriverService Members

    public ClaimsPrincipal? User => _currentUserService.User;
    public string? UserId => _currentUserService.UserId;
    public string? Email => _currentUserService.Email;
    public string? FirstName => _currentUserService.FirstName;
    public string? LastName => _currentUserService.LastName;
    public string? Username => _currentUserService.Username;
    public string? ClientId => _currentUserService.ClientId;
    public bool IsMachine => _currentUserService.IsMachine;

    public async Task<Driver?> GetDriver(CancellationToken cancellationToken = default) =>
        UserId is null ? null : await _driversProvider.GetDriver(new IdentityId(UserId), cancellationToken);

    #endregion
}