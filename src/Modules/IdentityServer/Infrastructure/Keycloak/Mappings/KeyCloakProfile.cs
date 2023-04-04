using Ark.IdentityServer.Infrastructure.Keycloak.Models;
using AutoMapper;
using KeyKloakObject = Ark.IdentityServer.Application.DTOs.KeyCloak;

namespace Ark.IdentityServer.Infrastructure.Keycloak.Mappings;

public class KeyCloakProfile : Profile
{
    public KeyCloakProfile()
    {
        //todo Authertification Response
        CreateMap<KeyCloakBaseObject, KeyKloakObject.ApplicationKeyCloakBaseObject>()
            .Include<KeyCloakAuthenticationError,KeyKloakObject.ApplicationKeyCloakAuthenticationError>()
            .Include<KeyCloakResponse,KeyKloakObject.ApplicationKeyCloakResponse>();
        CreateMap<KeyCloakAuthenticationError, KeyKloakObject.ApplicationKeyCloakAuthenticationError>();
        CreateMap<KeyCloakResponse, KeyKloakObject.ApplicationKeyCloakResponse>();
    }
}