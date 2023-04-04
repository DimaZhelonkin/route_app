using Ark.SharedLib.Common.CQS.Implementations;

namespace Ark.IdentityServer.Application.Features.Commands;

public record UpdateUserRolesCommand : CommandResult
{
    public UpdateUserRolesCommand(string username, List<string> roles)
    {
        Username = username;
        Roles = roles;
    }

    public string Username { get; init; }
    public List<string> Roles { get; init; }
}