using Ark.IdentityServer.Domain.ApplicationUser;
using Ark.IdentityServer.Infrastructure.Keycloak.Models;
using Ark.Infrastructure.Extensions;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Sdk.Admin.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Refit;

namespace Ark.IdentityServer.Infrastructure.Keycloak;

public interface IKeyCloakAuthenticationService
{
    Task<KeyCloakBaseObject> Authenticate(User user, CancellationToken cancellationToken = default);
    Task<KeyCloakBaseObject> Authenticate(string username, string password, CancellationToken cancellationToken = default);
    Task<KeyCloakBaseObject> Authenticate(ApplicationUser applicationUser, CancellationToken cancellationToken);
}

public class KeyCloakAuthenticationService : IKeyCloakAuthenticationService
{
    private readonly IKeycloakAuthClient _keycloakAuthClient;
    private readonly IOptions<KeycloakAuthenticationOptions> _keycloakAuthenticationOptions;
    private readonly ILogger<IKeyCloakAuthenticationService> _logger;

    public KeyCloakAuthenticationService(IKeycloakAuthClient keycloakAuthClient,
        IOptions<KeycloakAuthenticationOptions> keycloakAuthenticationOptions,
        ILogger<IKeyCloakAuthenticationService> logger)
    {
        _keycloakAuthClient = keycloakAuthClient;
        _keycloakAuthenticationOptions = keycloakAuthenticationOptions;
        _logger = logger;
    }

    #region IKeyCloakAuthenticationService Members

    public Task<KeyCloakBaseObject> Authenticate(User user, CancellationToken cancellationToken = default)
    {
        return Authenticate(user.Username!, user.Credentials!.First().Value!, cancellationToken);
    }

    public async Task<KeyCloakBaseObject> Authenticate(string username, string password,
        CancellationToken cancellationToken = default)
    {
        var realm = _keycloakAuthenticationOptions.Value.Realm;
        var clientId = _keycloakAuthenticationOptions.Value.Resource;
        var grantType = "password";
        var clientSecret = _keycloakAuthenticationOptions.Value.Credentials.Secret;
        var contentKey = new List<KeyValuePair<string, string>>
        {
            new("client_id", clientId),
            new("client_secret", clientSecret),
            new("grant_type", grantType),
            new("username", username),
            new("password", password),
        };

        var content = new FormUrlEncodedContent(contentKey);
        var res = await _keycloakAuthClient.Authenticate(realm, content);

        var stringResult = await res.Content.ReadAsStringAsync(cancellationToken);

        try
        {
            res.EnsureSuccessStatusCode().WriteRequestToConsole();
            var successResponse = JsonConvert.DeserializeObject<KeyCloakResponse>(stringResult);
            return successResponse;
        }
        catch (Exception e)
        {
            var errorResponse = JsonConvert.DeserializeObject<KeyCloakAuthenticationError>(stringResult);
            _logger.LogError("Error getting something fun to say: {Error}", e,errorResponse);
            throw;
        }
    }

    public Task<KeyCloakBaseObject> Authenticate(ApplicationUser applicationUser,
        CancellationToken cancellationToken)
    {
        return Authenticate(applicationUser.Username, applicationUser.Password, cancellationToken);
    }

    #endregion
}

[Headers("Content-Type: application/x-www-form-urlencoded")]
public interface IKeycloakAuthClient
{
    [Post("/realms/{realm}/protocol/openid-connect/token")]
    Task<HttpResponseMessage> Authenticate(string realm,
        [Body(BodySerializationMethod.UrlEncoded)]
        FormUrlEncodedContent content);
}

static internal class KeycloakClientApiConstants
{
    private const string AdminApiBase = "/admin";

    private const string Realms = "realms";

    internal const string GetRealm = $"{AdminApiBase}/{Realms}/{{realm}}";


    #region Resource API

    internal const string GetResources = "/realms/{realm}/authz/protection/resource_set";

    internal const string GetResource = $"{GetResources}/{{id}}";

    internal const string CreateResource = $"{GetResources}";

    internal const string PutResource = $"{GetResource}";

    internal const string GetResourceByExactName = "/realms/{realm}/authz/protection/resource_set?&exactName=true";

    #endregion

    #region User API

    internal const string GetUsers = $"{GetRealm}/users";

    internal const string GetUser = $"{GetRealm}/users/{{id}}";

    internal const string CreateUser = $"{GetRealm}/users";

    internal const string UpdateUser = $"{GetRealm}/users/{{id}}";

    internal const string SendVerifyEmail = $"{GetRealm}/users/{{id}}/send-verify-email";

    #endregion
}