using Ark.IdentityServer.Application.DTOs.KeyCloak;
using Ark.SharedLib.Common.CQS.Implementations;
using Newtonsoft.Json;

namespace Ark.IdentityServer.Application.Features.Authentication.ConfirmAuthorizationCode;

public record ConfirmAuthorizationCodeCommand : CommandResult<ApplicationKeyCloakBaseObject>
{
    public ConfirmAuthorizationCodeCommand(string phoneNumber, string code)
    {
        PhoneNumber = phoneNumber;
        Code = code;
    }

    public string PhoneNumber { get; set; }
    public string Code { get; }
}
