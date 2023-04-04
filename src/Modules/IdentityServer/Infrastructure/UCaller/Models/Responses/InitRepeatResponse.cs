using Ark.IdentityServer.Infrastructure.UCaller.Models.Requests;
using Refit;

namespace Ark.IdentityServer.Infrastructure.UCaller.Models.Responses;

public class InitRepeatResponse : BaseResponse
{
    /// <summary>
    ///     Уникальный ID в системе uCaller, который позволит проверять статус и инициализировать метод
    ///     <see cref="InitCallRequest" />>
    /// </summary>
    [AliasAs("ucaller_id")]
    public int UcallerId { get; set; }

    /// <summary>
    ///     Номер телефона, куда мы совершили звонок, номер телефона замаскирован, мы храним только хеш номера и маску.
    /// </summary>
    [AliasAs("phone")]
    public string Phone { get; set; }

    /// <summary>
    ///     Код, который будет последними цифрами в номере телефона
    ///     в ответе код всегда будет строкой так мы поддерживаем коды от 0001 до 9999, если 0001 вернуть в формате number он
    ///     обрежется до 1
    /// </summary>
    [AliasAs("code")]
    public string? Code { get; set; }

    /// <summary>
    ///     Идентификатор пользователя переданный клиентом
    /// </summary>
    [AliasAs("client")]
    public string? Client { get; set; }

    /// <summary>
    ///     Появляется только если вами был передан параметр `unique`
    /// </summary>
    [AliasAs("unique_request_id")]
    public string? UniqueRequestId { get; set; }

    /// <summary>
    ///     Появляется при переданном параметре `unique`, если такой запрос уже был инициализирован ранее
    /// </summary>
    [AliasAs("exists")]
    public bool? Exists { get; set; }

    /// <summary>
    ///     Статус повторной авторизации
    /// </summary>
    [AliasAs("free_repeated")]
    public bool FreeRepeated { get; set; }
}