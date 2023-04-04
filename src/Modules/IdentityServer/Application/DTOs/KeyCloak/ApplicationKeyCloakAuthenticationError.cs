using Newtonsoft.Json;

namespace Ark.IdentityServer.Application.DTOs.KeyCloak;

public class ApplicationKeyCloakAuthenticationError : ApplicationKeyCloakBaseObject
{
    [JsonProperty("error")]
    public string Error { get; set; }
    
    [JsonProperty("error_description")]
    public string ErrorDescription { get; set; }
}