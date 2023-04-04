using Refit;

namespace Ark.IdentityServer.Infrastructure.UCaller.Models.Responses;

public class BaseResponse
{
    /// <summary>
    ///     Статус выполнения запроса
    /// </summary>
    [AliasAs("status")]
    public bool Status { get; set; }
}