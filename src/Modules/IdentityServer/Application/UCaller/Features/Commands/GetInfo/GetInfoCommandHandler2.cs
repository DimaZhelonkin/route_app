using Ark.IdentityServer.Application.Contracts;
using Ark.SharedLib.Common.CQS.Implementations;
using Ark.SharedLib.Common.Results;

namespace Ark.IdentityServer.Application.UCaller.Features.Commands.GetInfo;

public class GetInfoCommandHandler : CommandHandler<GetInfoCommand, GetInfoResponse>
{
    private readonly IPhoneAuthService _phoneAuthService;

    public GetInfoCommandHandler(IPhoneAuthService phoneAuthService)
    {
        _phoneAuthService = phoneAuthService;
    }

    public override Task<Result<GetInfoResponse>> Handle(GetInfoCommand command,
        CancellationToken cancellationToken = default) =>
        _phoneAuthService.GetInfo(command.Uid, cancellationToken);
}