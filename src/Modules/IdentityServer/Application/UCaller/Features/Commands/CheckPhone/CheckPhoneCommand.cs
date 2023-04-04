using Ark.IdentityServer.Application.Contracts;
using Ark.SharedLib.Common.CQS.Implementations;
using Ark.SharedLib.Common.Results;

namespace Ark.IdentityServer.Application.UCaller.Features.Commands.CheckPhone;

public record CheckPhoneCommand : CommandResult<CheckPhoneResponse>
{
    public string Phone { get; set; }
}

public record CheckPhoneResponse
{
    public string Source { get; set; }

    public string? Error { get; set; }

    public int Mobile { get; set; }

    public int Phone { get; set; }

    public string? CountryIso { get; set; }

    public int CountryCode { get; set; }

    public int Mnc { get; set; }

    public int number { get; set; }

    public string? Provider { get; set; }

    public string? Company { get; set; }

    public string? Country { get; set; }

    public string? Region { get; set; }

    public string? City { get; set; }

    public string? PhoneFormat { get; set; }

    public int Cost { get; set; }

    public int Balance { get; set; }
}

public class CheckPhoneCommandHanlder : CommandHandler<CheckPhoneCommand, CheckPhoneResponse>
{
    private readonly IPhoneAuthService _phoneAuthService;

    public CheckPhoneCommandHanlder(IPhoneAuthService phoneAuthService)
    {
        _phoneAuthService = phoneAuthService;
    }

    public override Task<Result<CheckPhoneResponse>> Handle(CheckPhoneCommand command,
        CancellationToken cancellationToken = default) =>
        _phoneAuthService.CheckPhone(command.Phone, cancellationToken);
}