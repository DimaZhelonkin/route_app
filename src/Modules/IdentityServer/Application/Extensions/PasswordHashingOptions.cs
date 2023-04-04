namespace Ark.IdentityServer.Application.Extensions;

public sealed class PasswordHashingOptions
{
    public static string Section = "PasswordHashing";
    public int Iterations { get; set; } = 10000;
}