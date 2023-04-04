using Ark.IdentityServer.Application.Contracts;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Sdk.Admin;
using Keycloak.AuthServices.Sdk.Admin.Models;
using Keycloak.AuthServices.Sdk.Admin.Requests.Users;
using Microsoft.Extensions.Options;

namespace Ark.IdentityServer.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IOptions<KeycloakAuthenticationOptions> _keycloakAuthenticationOptions;
    private readonly IKeycloakUserClient _keycloakUserClient;

    public UserService(IKeycloakUserClient keycloakUserClient,
        IOptions<KeycloakAuthenticationOptions> keycloakAuthenticationOptions)
    {
        _keycloakUserClient = keycloakUserClient;
        _keycloakAuthenticationOptions = keycloakAuthenticationOptions;
    }

    #region IUserService Members

    public async Task<User> CreateUser(User user)
    {
        // TODO create our own keycloak client implementation with necessary methods 
        var response = await _keycloakUserClient.CreateUser(_keycloakAuthenticationOptions.Value.Realm, user);
        var result = await response.Content.ReadAsStringAsync();
        return user;
    }

    public Task<User> GetUser(string userId) =>
        _keycloakUserClient.GetUser(_keycloakAuthenticationOptions.Value.Realm, userId);

    public async Task<User> GetUserByUsername(string username)
    {
        var parameters = new GetUsersRequestParameters
        {
            Username = username,
        };
        var user = (await _keycloakUserClient.GetUsers(_keycloakAuthenticationOptions.Value.Realm, parameters))
            .FirstOrDefault();
        return user;
    }

    public async Task<IEnumerable<User>> GetUsers(GetUsersRequestParameters parameters = null) =>
        await _keycloakUserClient.GetUsers(_keycloakAuthenticationOptions.Value.Realm, parameters);

    public Task SendVerifyEmail() => throw new NotImplementedException();

    #endregion
}