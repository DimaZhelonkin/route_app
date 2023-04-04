namespace Ark.IdentityServer.Application.UCaller.Features.Commands.InitCall;

public class InitCallResponse
{
    public string Phone { get; set; }
    public string? Code { get; set; }

    public int UCallerId { get; set; }

    public string? Client { get; set; }

    public string? UniqueRequestId { get; set; }

    public bool? Exists { get; set; }
}