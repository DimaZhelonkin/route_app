using Ark.IdentityServer.Domain.ApplicationUser;
using Ark.IdentityServer.Domain.Roles;
using Ark.SharedLib.Common.Options;

namespace Ark.IdentityServer.DomainServices.Services;

public interface IApplicationUserService
{
    /// <summary>
    ///     Creates a new user according to the provided <paramref name="user" />
    /// </summary>
    /// <param name="user"></param>
    /// <param name="password"></param>
    /// <param name="roles"></param>
    /// <param name="isActive"></param>
    /// <returns></returns>
    Task CreateUserAsync(ApplicationUser user, string password, List<string>? roles, bool isActive);

    /// <summary>
    ///     Activates the application user with username <paramref name="username" />
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    Task ActivateUser(string username);

    /// <summary>
    ///     Deactivates the application user with username <paramref name="username" />
    /// </summary>
    /// <returns></returns>
    Task DeactivateUser(string username);

    /// <summary>
    ///     Adds a role to a user
    /// </summary>
    /// <returns></returns>
    Task AddRoles(IEnumerable<Role> roles);

    /// <summary>
    ///     Removes a role from a user
    /// </summary>
    /// <returns></returns>
    Task RemoveRoles(IEnumerable<Role> roles);

    /// <summary>
    ///     Updates a user's roles
    /// </summary>
    /// <param name="username">the username</param>
    /// <param name="roles">the list of new roles</param>
    /// <returns></returns>
    Task UpdateRoles(string username, List<string> roles);

    /// <summary>
    ///     Retrieves all users
    /// </summary>
    /// <returns>list of <see cref="ApplicationUser" /></returns>
    Task<List<ApplicationUser>> GetAllUsersAsync(QueryParameters parameters,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     Get user
    /// </summary>
    /// <param name="id">the user ID</param>
    /// <param name="cancellationToken"></param>
    /// <returns> a <see cref="ApplicationUser" /></returns>
    Task<ApplicationUser> GetUserAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Changes a user's password
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    Task ChangePassword(string username, string password);
}