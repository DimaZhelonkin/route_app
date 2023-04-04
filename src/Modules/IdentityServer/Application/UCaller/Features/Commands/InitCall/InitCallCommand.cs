using Ark.SharedLib.Common.CQS.Implementations;

namespace Ark.IdentityServer.Application.UCaller.Features.Commands.InitCall;

public record InitCallCommand : CommandResult<InitCallResponse>
{
    public InitCallCommand(ulong phone)
    {
        Phone = phone;
    }

    public ulong Phone { get; set; }

    public string? Code { get; set; }

    public string? Client { get; set; }

    public string? Unique { get; set; }

    public bool? Voice { get; set; }
}