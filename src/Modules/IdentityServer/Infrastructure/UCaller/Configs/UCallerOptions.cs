namespace Ark.IdentityServer.Infrastructure.UCaller.Configs;

public class UCallerOptions
{
    public const string Section = "UCaller";

    public string Url { get; set; }
    public string ServiceId { get; set; }
    public string SecretKey { get; set; }
}