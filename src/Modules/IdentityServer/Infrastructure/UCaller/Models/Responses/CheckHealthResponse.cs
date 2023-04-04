using Refit;

namespace Ark.IdentityServer.Infrastructure.UCaller.Models.Responses;

public class CheckHealthResponse : BaseResponse
{
    /// <summary>
    ///     Состояние всей инфраструктуры
    /// </summary>
    [AliasAs("status")]
    public bool Status { get; set; }

    /// <summary>
    ///     Состояния базы данных
    /// </summary>
    [AliasAs("database")]
    public bool Database { get; set; }

    /// <summary>
    ///     Состояние провайдеров телефонии
    /// </summary>
    [AliasAs("providers")]
    public bool Providers { get; set; }
}