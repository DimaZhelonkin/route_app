using Ark.SharedLib.Common.Results;

namespace Ark.SharedLib.Application.Abstractions;

public interface IHttpClientLogger
{
    /// <summary>
    ///     Идентификатор на случай, если логи нужно привязать к чему-либо
    /// </summary>
    void SetUniqueId(Guid? guid);

    /// <summary>
    ///     Идентификатор на случай, если логи нужно привязать к чему-либо
    /// </summary>
    void SetUniqueId(long? id);

    Task<Result> Log(HttpResponseMessage msg);
}