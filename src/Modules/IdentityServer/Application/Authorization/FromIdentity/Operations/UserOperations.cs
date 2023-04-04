using Ark.IdentityServer.Application.Authorization.FromIdentity.Requirements;

namespace Ark.IdentityServer.Application.Authorization.FromIdentity.Operations;

/// <summary>
///     Holds <see cref="UserOperationAuthorizationRequirement" />s for user
/// </summary>
public static class UserOperations
{
    public static UserOperationAuthorizationRequirement Create = new() {Name = OPERATIONS.USER.CREATE};

    public static UserOperationAuthorizationRequirement Read() => new UserOperationAuthorizationRequirement
        {Name = OPERATIONS.USER.READ};

    public static UserOperationAuthorizationRequirement Edit() => new UserOperationAuthorizationRequirement
        {Name = OPERATIONS.USER.EDIT};

    public static UserOperationAuthorizationRequirement Delete() => new UserOperationAuthorizationRequirement
        {Name = OPERATIONS.USER.DELETE};
}