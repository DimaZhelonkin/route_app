using Keycloak.AuthServices.Sdk.Admin.Models;
using Keycloak.AuthServices.Sdk.Admin.Requests.Users;

namespace Ark.IdentityServer.Application.Contracts;

public interface IUserService
{
    /// <summary>
    ///     Create a new user.
    /// </summary>
    /// <remarks>
    ///     Username must be unique.
    /// </remarks>
    /// <param name="user">User representation.</param>
    /// <returns></returns>
    Task<User> CreateUser(User user);

    Task<User> GetUser(string userId);
    Task<User> GetUserByUsername(string username);
    Task<IEnumerable<User>> GetUsers(GetUsersRequestParameters parameters = null);

    /// <summary>
    ///     Send an email-verification email to the user.
    /// </summary>
    /// <remarks>
    ///     An email contains a link the user can click to verify their email address.
    /// </remarks>
    /// <returns></returns>
    Task SendVerifyEmail();
}