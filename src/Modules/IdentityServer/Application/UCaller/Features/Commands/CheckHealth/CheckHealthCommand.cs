using Ark.IdentityServer.Application.Contracts;
using Ark.SharedLib.Common.CQS.Implementations;
using Ark.SharedLib.Common.Results;

namespace Ark.IdentityServer.Application.UCaller.Features.Commands.CheckHealth;

public record CheckHealthCommand : CommandResult<CheckHealthResponse>
{
}

public class CheckHealthResponse
{
    public bool Status { get; set; }

    public bool Database { get; set; }

    public bool Providers { get; set; }
}

public class CheckHealthCommandHandler : CommandHandler<CheckHealthCommand, CheckHealthResponse>
{
    private readonly IPhoneAuthService _phoneAuthService;

    public CheckHealthCommandHandler(IPhoneAuthService phoneAuthService)
    {
        _phoneAuthService = phoneAuthService;
    }

    public override Task<Result<CheckHealthResponse>> Handle(CheckHealthCommand command,
        CancellationToken cancellationToken = default) =>
        _phoneAuthService.CheckHealth(cancellationToken);
}