using Ark.SharedLib.Common.CQS.Implementations;

namespace Ark.IdentityServer.Application.Features.Commands;

public record ActivateUserCommand : CommandResult
{
    public ActivateUserCommand(string username)
    {
        Username = username;
    }

    public string Username { get; init; }
}