using Ark.SharedLib.Common.CQS.Implementations;

namespace Ark.IdentityServer.Application.Features.Commands;

public record DeactivateUserCommand : CommandResult
{
    public DeactivateUserCommand(string username)
    {
        Username = username;
    }

    public string Username { get; init; }
}