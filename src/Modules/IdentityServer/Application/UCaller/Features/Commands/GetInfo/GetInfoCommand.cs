using Ark.SharedLib.Common.CQS.Implementations;

namespace Ark.IdentityServer.Application.UCaller.Features.Commands.GetInfo;

public record GetInfoCommand : CommandResult<GetInfoResponse>
{
    public int Uid { get; set; }
}

public class GetInfoResponse
{
    public bool Status { get; set; }

    public int UcallerId { get; set; }

    public int InitTime { get; set; }

    public int CallStatus { get; set; }

    public bool IsRepeated { get; set; }

    public int FirstUcallerId { get; set; }

    public bool Repeatable { get; set; }

    public int? RepeatTimes { get; set; }

    public int[]? RepeatedUcallerIds { get; set; }

    public string? Unique { get; set; }

    public string? Phone { get; set; }

    public string? Code { get; set; }

    public string? Client { get; set; }

    public string? CountryCode { get; set; }

    public PhoneInfo? PhoneInfo { get; set; }

    public int Cost { get; set; }

    public int Balance { get; set; }
}

public class PhoneInfo
{
    public string? Operator { get; set; }

    public string? Region { get; set; }

    public string? Mnp { get; set; }
}