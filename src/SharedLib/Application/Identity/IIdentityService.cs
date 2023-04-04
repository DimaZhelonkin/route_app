namespace Ark.SharedLib.Application.Identity;

public interface IIdentityService : ICurrentUserService
{
    Task<bool> AuthorizeAsync(string policyName);

    Task<bool> AuthorizeAsync(object resource, string policyName);

    bool IsInRoleAsync(string role);
}