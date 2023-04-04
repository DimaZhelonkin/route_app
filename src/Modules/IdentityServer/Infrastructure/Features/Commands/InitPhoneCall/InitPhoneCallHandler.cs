using Ark.IdentityServer.Application.Contracts;
using Ark.IdentityServer.Application.Features.Authentication.InitPhoneCall;
using Ark.IdentityServer.Application.UCaller.Features.Commands.InitCall;
using Ark.SharedLib.Common.CQS.Implementations;
using Ark.SharedLib.Common.Results;

namespace Ark.IdentityServer.Infrastructure.Features.Commands.InitPhoneCall;

public class
    InitPhoneCallHandler : CommandHandler<InitPhoneCallCommand>
{
    private readonly IPhoneAuthService _phoneAuthService;

    public InitPhoneCallHandler(IPhoneAuthService phoneAuthService)
    {
        _phoneAuthService = phoneAuthService;
    }

    public override async Task<Result> Handle(
        InitPhoneCallCommand command,
        CancellationToken cancellationToken = default)
    {
        if (!ulong.TryParse(command.PhoneNumber, out var phoneNumber))
            return Result.Error("InitPhoneCallCommand", $"Couldn't parse {command.PhoneNumber} to ulong");
        var response = await _phoneAuthService.InitCall(new InitCallCommand(phoneNumber), cancellationToken);
        return response.Void();
    }
}