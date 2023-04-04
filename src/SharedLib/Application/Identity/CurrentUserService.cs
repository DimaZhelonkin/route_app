using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Ark.SharedLib.Application.Identity;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    #region ICurrentUserService Members

    public ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;
    public string? UserId => _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    public string? Email => _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;
    public string? FirstName => _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.GivenName)?.Value;
    public string? LastName => _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Surname)?.Value;

    public string? Username => _httpContextAccessor.HttpContext
                                                   ?.User
                                                   ?.Claims
                                                   ?.FirstOrDefault(x => x.Type is "preferred_username" or "username")
                                                   ?.Value;

    public string? ClientId => _httpContextAccessor.HttpContext
                                                   ?.User
                                                   ?.Claims
                                                   ?.FirstOrDefault(x => x.Type is "client_id" or "clientId")
                                                   ?.Value;

    public bool IsMachine => ClientId != null;

    #endregion
}