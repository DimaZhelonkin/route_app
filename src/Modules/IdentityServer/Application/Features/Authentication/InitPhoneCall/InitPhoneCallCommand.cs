using Ark.SharedLib.Common.CQS.Implementations;

namespace Ark.IdentityServer.Application.Features.Authentication.InitPhoneCall;

public record InitPhoneCallCommand : CommandResult
{
    public InitPhoneCallCommand(string phoneNumber)
    {
        PhoneNumber = phoneNumber;
    }

    public string PhoneNumber { get; }
}