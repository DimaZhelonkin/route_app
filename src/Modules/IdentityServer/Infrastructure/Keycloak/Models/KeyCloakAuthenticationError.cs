using Newtonsoft.Json;

namespace Ark.IdentityServer.Infrastructure.Keycloak.Models;

/// <summary>
/// todo comments for object and property comments
/// </summary>
public class KeyCloakAuthenticationError:KeyCloakBaseObject
{
    [JsonProperty("error")]
    public string Error { get; set; }

    [JsonProperty("error_description")]
    public string ErrorDescription { get; set; }
}