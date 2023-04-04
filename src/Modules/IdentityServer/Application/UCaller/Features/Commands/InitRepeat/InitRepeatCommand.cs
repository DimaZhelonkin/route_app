using Ark.IdentityServer.Application.Contracts;
using Ark.SharedLib.Common.CQS.Implementations;
using Ark.SharedLib.Common.Results;

namespace Ark.IdentityServer.Application.UCaller.Features.Commands.InitRepeat;

public record InitRepeatCommand : CommandResult<InitRepeatResponse>
{
    public int Uid { get; set; }
}

public class InitRepeatResponse
{
    public bool Status { get; set; }

    public int UcallerId { get; set; }

    public string Phone { get; set; }

    public string? Code { get; set; }

    public string? Client { get; set; }


    public string? UniqueRequestId { get; set; }


    public bool? Exists { get; set; }


    public bool FreeRepeated { get; set; }
}

public class InitCallCommandHandler : CommandHandler<InitRepeatCommand, InitRepeatResponse>
{
    private readonly IPhoneAuthService _phoneAuthService;

    public InitCallCommandHandler(IPhoneAuthService phoneAuthService)
    {
        _phoneAuthService = phoneAuthService;
    }

    public override Task<Result<InitRepeatResponse>> Handle(InitRepeatCommand command,
        CancellationToken cancellationToken = default) =>
        _phoneAuthService.InitResponse(command.Uid, cancellationToken);
}