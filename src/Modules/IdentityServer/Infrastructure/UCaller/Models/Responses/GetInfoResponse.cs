using Refit;

namespace Ark.IdentityServer.Infrastructure.UCaller.Models.Responses;

public class GetInfoResponse : BaseResponse
{
    /// <summary>
    ///     Статус выполнения запроса <see cref="InitCallRequest" />>
    /// </summary>
    [AliasAs("status")]
    public bool Status { get; set; }

    /// <summary>
    ///     Уникальный ID в системе uCaller, который позволит проверять статус и инициализировать метод
    ///     <see cref="InitCallRequest" />>
    /// </summary>
    [AliasAs("ucaller_id")]
    public int UcallerId { get; set; }

    /// <summary>
    ///     Дата инициализации авторизации Unix Timestamp
    /// </summary>
    [AliasAs("init_time")]
    public int InitTime { get; set; }

    /// <summary>
    ///     Статус звонка, -1 = информация проверяется (от 1 сек до 1 минуты), 0 = дозвониться не удалось, 1 = звонок
    ///     осуществлен
    /// </summary>
    [AliasAs("call_status")]
    public int CallStatus { get; set; }

    /// <summary>
    ///     Является ли этот uCaller ID повтором (<see cref="InitRepeatResponse" />), если да, будет добавлен first_ucaller_id
    ///     с первым uCaller ID этой цепочки
    /// </summary>
    [AliasAs("is_repeated")]
    public bool IsRepeated { get; set; }

    /// <summary>
    ///     Идентификатор первой авторизации из сессии повторов
    /// </summary>
    [AliasAs("first_ucaller_id")]
    public int FirstUcallerId { get; set; }

    /// <summary>
    ///     Возможно ли инициализировать бесплатные повторы (<see cref="InitRepeatResponse" />)
    /// </summary>
    [AliasAs("repeatable")]
    public bool Repeatable { get; set; }

    /// <summary>
    ///     Остаток бесплатных повторов авторизации, появляется в случае repeatable: true
    /// </summary>
    [AliasAs("repeat_times")]
    public int? RepeatTimes { get; set; }

    /// <summary>
    ///     Массив идентификаторов инициализированных повторов (<see cref="InitRepeatResponse" />)
    /// </summary>
    [AliasAs("repeated_ucaller_ids")]
    public int[]? RepeatedUcallerIds { get; set; }

    /// <summary>
    ///     Ключ идемпотентности (если был передан)
    /// </summary>
    [AliasAs("unique")]
    public string? Unique { get; set; }

    /// <summary>
    ///     Номер телефона, куда мы совершили звонок, номер телефона замаскирован, мы храним только хеш номера и маску.
    /// </summary>
    [AliasAs("phone")]
    public string? Phone { get; set; }

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
    ///     ISO код страны номера телефона на который был совершен звонок
    /// </summary>
    [AliasAs("country_code")]
    public string? CountryCode { get; set; }

    /// <summary>
    ///     Информация о номере телефона, может быть пустой
    /// </summary>
    [AliasAs("phone_info")]
    public PhoneInfo? PhoneInfo { get; set; }

    /// <summary>
    ///     Стоимость авторизации
    /// </summary>
    [AliasAs("cost")]
    public int Cost { get; set; }

    /// <summary>
    ///     Состояние баланса до списания этой операции
    /// </summary>
    [AliasAs("balance")]
    public int Balance { get; set; }
}

public class PhoneInfo
{
    /// <summary>
    ///     Оператор связи
    /// </summary>
    [AliasAs("operator")]
    public string? Operator { get; set; }

    /// <summary>
    ///     Регион субъеккта Российской федерации
    /// </summary>
    [AliasAs("region")]
    public string? Region { get; set; }

    /// <summary>
    ///     Текущий оператор связи
    /// </summary>
    [AliasAs("mnp")]
    public string? Mnp { get; set; }
}