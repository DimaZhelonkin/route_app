using Newtonsoft.Json;

namespace Ark.IdentityServer.Infrastructure.Keycloak.Models;

/// <summary>
///  todo comments and property comments
/// </summary>
public class KeyCloakResponse:KeyCloakBaseObject
{
    [JsonProperty("access_token")]
    public string AccessToken { get; set; }
    
    [JsonProperty("scope")]
    public string Scope { get; set; }

    [JsonProperty("expires_in")]
    public string ExpiresIn { get; set; }

    [JsonProperty("refresh_expires_in")]
    public string RefreshExpiresIn { get; set; }

    [JsonProperty("refresh_token")]
    public string RefreshToken { get; set; }

    [JsonProperty("token_type")]
    public string TokenType { get; set; }

    [JsonProperty("id_token")]
    public string IdToken { get; set; }

    [JsonProperty("session_state")]
    public string SessionState { get; set; }
}