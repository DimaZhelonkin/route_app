using Ark.SharedLib.Common.CQS.Implementations;

namespace Ark.IdentityServer.Application.Features.Authentication.Login;

public record LoginCommand : CommandResult<string>
{
    public LoginCommand(ulong phoneNumber)
    {
        PhoneNumber = phoneNumber;
    }

    public ulong PhoneNumber { get; set; }
}