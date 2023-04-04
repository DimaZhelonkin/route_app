using System.Security.Claims;
using Ark.IdentityServer.Domain.ApplicationUser;
using Ark.Rides.Application.Providers;
using Ark.Rides.Domain.Aggregates.Passenger;
using Ark.SharedLib.Application.Identity;

namespace Ark.Rides.Application.Services;

public class CurrentPassengerService : ICurrentPassengerService
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IPassengersProvider _passengersProvider;

    public CurrentPassengerService(ICurrentUserService currentUserService, IPassengersProvider passengersProvider)
    {
        _currentUserService = currentUserService;
        _passengersProvider = passengersProvider;
    }

    #region ICurrentPassengerService Members

    public ClaimsPrincipal? User => _currentUserService.User;
    public string? UserId => _currentUserService.UserId;
    public string? Email => _currentUserService.Email;
    public string? FirstName => _currentUserService.FirstName;
    public string? LastName => _currentUserService.LastName;
    public string? Username => _currentUserService.Username;
    public string? ClientId => _currentUserService.ClientId;
    public bool IsMachine => _currentUserService.IsMachine;

    public async Task<Passenger?> GetPassenger(CancellationToken cancellationToken = default) =>
        UserId is null
            ? null
            : await _passengersProvider.GetPassengerByIdentityId(new IdentityId(UserId), cancellationToken);

    #endregion
}