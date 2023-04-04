using Refit;

namespace Ark.IdentityServer.Infrastructure.UCaller.Models.Requests;

public class CheckPhoneRequest
{
    /// <summary>
    ///     Номер телефона
    /// </summary>
    [AliasAs("phone")]
    public string Phone { get; set; }
}