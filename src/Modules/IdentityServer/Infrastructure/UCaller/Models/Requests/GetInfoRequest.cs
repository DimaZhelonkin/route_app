using Refit;

namespace Ark.IdentityServer.Infrastructure.UCaller.Models.Requests;

public class GetInfoRequest
{
    /// <summary>
    ///     Идентификатор UcallerId из объекта <see cref="InitCallResponse" />
    /// </summary>
    [AliasAs("uid")]
    public int Uid { get; set; }
}