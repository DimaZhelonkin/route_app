using Ark.IdentityServer.Application.Contracts;
using Ark.SharedLib.Common.CQS.Implementations;
using Ark.SharedLib.Common.Results;

namespace Ark.IdentityServer.Application.UCaller.Features.Commands.InitCall;

public class InitCallCommandHandler : CommandHandler<InitCallCommand, InitCallResponse>
{
    private readonly IPhoneAuthService _phoneAuthService;

    public InitCallCommandHandler(IPhoneAuthService phoneAuthService)
    {
        _phoneAuthService = phoneAuthService;
    }

    public override Task<Result<InitCallResponse>> Handle(InitCallCommand command,
        CancellationToken cancellationToken = default) =>
        _phoneAuthService.InitCall(command, cancellationToken);
}