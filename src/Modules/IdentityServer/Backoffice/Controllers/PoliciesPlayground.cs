using Ark.IdentityServer.Application;
using Ark.Infrastructure.Shared.Controllers;
using Keycloak.AuthServices.Authorization;
using Keycloak.AuthServices.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ark.IdentityServer.Backoffice.Controllers;

[Route("/policies-playground")]
public class PoliciesPlayground : ApiBaseController
{
    /// <summary>
    ///     Roles are mapped from keycloak claims.
    ///     Use keycloak mappers to implement RBAC.
    /// </summary>
    [Authorize(Roles = "Manager, Guest")]
    [HttpGet("role-based")]
    public void RoleCheck() => Ok();

    /// <summary>
    ///     Named policy registered via policyBuilder.
    ///     Default policy builder should be registered in DI.
    /// </summary>
    [Authorize(Policy = PolicyConstants.MyCustomPolicy)]
    [HttpGet("custom-policy-from-builder")]
    public void CustomPolicy() => Ok();

    /// <summary>
    ///     Policies used by custom authorize attributes should be resolved.
    ///     Create attribute derived from <see cref="AuthorizeAttribute" />
    /// </summary>
    [ProtectedResource("workspaces", "workspaces:read")]
    [HttpGet("custom-policy-attribute")]
    public void CustomPolicyFromAttribute() => Ok();

    /// <summary>
    ///     Based on automatically registered policies by <see cref="ProtectedResourcePolicyProvider" />
    /// </summary>
    [Authorize(Policy = "workspaces#workspaces:read")]
    [HttpGet("auto-registered-policy")]
    public void AutoPolicy() => Ok();

    [HttpGet("authorization-services")]
    public async Task<IActionResult> AspNetCoreAuthorizationServices(
        [FromQuery] string? resource,
        [FromQuery] string? scope,
        [FromServices] IAuthorizationService authorizationService)
    {
        // var workspace = new Workspace() {Id = id ?? Guid.Parse("1d654851-ff72-42b6-bd4a-468f95f61c7a")};
        // var requirement = new DecisionRequirement($"workspaces/{workspace.Id}", "read");
        var requirement = new DecisionRequirement(
            resource ?? "workspaces", scope ?? "workspaces:read");

        var accessed = await authorizationService
            .AuthorizeAsync(User, null, requirement);

        return !accessed.Succeeded ? Forbid() : Ok();
    }

    #region Nested type: ProtectedResourceAttribute

    private class ProtectedResourceAttribute : AuthorizeAttribute
    {
        public ProtectedResourceAttribute(string resource, string scope)
        {
            Policy = ProtectedResourcePolicy.From(resource, scope);
        }
    }

    #endregion
}