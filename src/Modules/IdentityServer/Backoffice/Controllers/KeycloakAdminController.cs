using Ark.Infrastructure.Shared.Controllers;
using Keycloak.AuthServices.Sdk.Admin;
using Keycloak.AuthServices.Sdk.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ark.IdentityServer.Backoffice.Controllers;

[Route("keycloak-api")]
public class KeycloakAdminController : ApiBaseController
{
    private const string DefaultRealm = "authz";
    private readonly IKeycloakRealmClient keycloakRealmClient;
    private readonly IKeycloakProtectedResourceClient protectedResourceClient;

    public KeycloakAdminController(
        IKeycloakRealmClient keycloakRealmClient,
        IKeycloakProtectedResourceClient protectedResourceClient)
    {
        this.keycloakRealmClient = keycloakRealmClient;
        this.protectedResourceClient = protectedResourceClient;
    }

    [HttpGet("realms")]
    public async Task<IActionResult> GetRealms() => Ok(await keycloakRealmClient.GetRealm(DefaultRealm));

    [HttpGet("resources")]
    public async Task<IActionResult> GetResources() => Ok(await protectedResourceClient.GetResources(DefaultRealm));

    [HttpGet("resources/{id}")]
    public async Task<IActionResult> GetResource(string id) =>
        Ok(await protectedResourceClient.GetResource(DefaultRealm, id));

    [HttpPost("resources")]
    public async Task<IActionResult> CreateResource()
    {
        var resource = new Resource($"workspaces/{Guid.NewGuid()}", new[] {"workspaces:read", "workspaces:delete"})
        {
            Attributes = {["test"] = "Owner, Operations"}, Type = "urn:workspace-authz:resource:workspaces",
        };
        return Ok(await protectedResourceClient.CreateResource(DefaultRealm, resource));
    }
}