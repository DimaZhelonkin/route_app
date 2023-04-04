using Ark.IdentityServer.Infrastructure.UCaller.Models.Responses;
using Refit;

namespace Ark.IdentityServer.Infrastructure.UCaller.Models.Requests;

public class InitRepeatRequest
{
    /// <summary>
    ///     Идентификатор UcallerId из объекта <see cref="InitCallResponse" />
    /// </summary>
    [AliasAs("uid")]
    public int Uid { get; set; }
}