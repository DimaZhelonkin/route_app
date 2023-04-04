using System.Security.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Ark.SharedLib.Application.Identity;

public class IdentityService : IIdentityService
{
    private readonly IAuthorizationService _authorizationService;
    private readonly ICurrentUserService _userService;

    public IdentityService(IAuthorizationService authorizationService, ICurrentUserService userService)
    {
        _authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }

    #region IIdentityService Members

    public Task<bool> AuthorizeAsync(string policyName)
    {
        var principal = GetPrincipal();
        return AuthorizeAsync(principal, policyName);
    }


    public async Task<bool> AuthorizeAsync(object resource, string policyName)
    {
        var principal = GetPrincipal();
        var result = await _authorizationService
            .AuthorizeAsync(principal, resource, policyName);

        return result.Succeeded;
    }

    public bool IsInRoleAsync(string role) => Principal?.IsInRole(role) ?? false;

    #endregion

    private async Task<bool> AuthorizeAsync(ClaimsPrincipal principal, string policyName)
    {
        var result = await _authorizationService
            .AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }

    private ClaimsPrincipal GetPrincipal()
    {
        var principal = _userService?.User
                        ?? throw new AuthenticationException("Couldn't find principal. Please authenticate");
        return principal;
    }

    #region ICurrentUserService

    public ClaimsPrincipal? User { get; }
    public string? UserId => _userService.UserId;
    public string? Email => _userService.Email;
    public string? FirstName => _userService.FirstName;
    public string? LastName => _userService.LastName;

    public string? Username => _userService.Username;
    public string? ClientId => _userService.ClientId;
    public bool IsMachine => _userService.IsMachine;

    public ClaimsPrincipal? Principal => _userService.User;

    #endregion
}