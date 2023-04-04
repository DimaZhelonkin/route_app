using Ark.Infrastructure.Shared.Controllers;
using Keycloak.AuthServices.Sdk.AuthZ;
using Microsoft.AspNetCore.Mvc;

namespace Ark.IdentityServer.Backoffice.Controllers;

[Route("authz")]
public class KeycloakAuthZController : ApiBaseController
{
    private readonly IKeycloakProtectionClient protectionClient;

    public KeycloakAuthZController(IKeycloakProtectionClient protectionClient)
    {
        this.protectionClient = protectionClient;
    }

    [HttpGet("try-resource")]
    public async Task<IActionResult> VerifyAccess(
        [FromQuery] string? resource,
        [FromQuery] string? scope,
        CancellationToken cancellationToken)
    {
        var verified = await protectionClient
            .VerifyAccessToResource(resource ?? "workspaces", scope ?? "workspaces:read", cancellationToken);

        return Ok(verified);
    }
}