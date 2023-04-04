using Refit;

namespace Ark.IdentityServer.Infrastructure.UCaller.Models.Responses;

public class CheckPhoneResponse : BaseResponse
{
    /// <summary>
    ///     Исходный телефон одной строкой
    /// </summary>
    [AliasAs("source")]
    public string Source { get; set; }

    /// <summary>
    ///     Ошибка появляется в случае не валидного номера телефона
    /// </summary>
    [AliasAs("error")]
    public string? Error { get; set; }

    /// <summary>
    ///     Тип номерая, мобильный = 1, не мобильный = 0
    /// </summary>
    [AliasAs("mobile")]
    public int Mobile { get; set; }

    /// <summary>
    ///     Номер телефона в формате E.164
    /// </summary>
    [AliasAs("phone")]
    public int Phone { get; set; }

    /// <summary>
    ///     ISO код страны ISO 3166-1 alpha-2
    /// </summary>
    [AliasAs("country_iso")]
    public string? CountryIso { get; set; }

    /// <summary>
    ///     Код страны
    /// </summary>
    [AliasAs("country_code")]
    public int CountryCode { get; set; }

    /// <summary>
    ///     Код мобильной сети
    /// </summary>
    [AliasAs("mnc")]
    public int Mnc { get; set; }

    /// <summary>
    ///     Локальный номер телефона без кода страны
    /// </summary>
    [AliasAs("number")]
    public int number { get; set; }

    /// <summary>
    ///     Оператор связи
    /// </summary>
    [AliasAs("provider")]
    public string? Provider { get; set; }

    /// <summary>
    ///     Компания (огранизация) оператора связи (только для России)
    /// </summary>
    [AliasAs("company")]
    public string? Company { get; set; }

    /// <summary>
    ///     Страна
    /// </summary>
    [AliasAs("country")]
    public string? Country { get; set; }

    /// <summary>
    ///     Регион (только для России)
    /// </summary>
    [AliasAs("region")]
    public string? Region { get; set; }

    /// <summary>
    ///     Город (только для России)
    /// </summary>
    [AliasAs("city")]
    public string? City { get; set; }

    /// <summary>
    ///     Номер телефона в национальном формате
    /// </summary>
    [AliasAs("phone_format")]
    public string? PhoneFormat { get; set; }

    /// <summary>
    ///     Стоимость услуги
    /// </summary>
    [AliasAs("cost")]
    public int Cost { get; set; }

    /// <summary>
    ///     Текущий баланс аккаунта
    /// </summary>
    [AliasAs("balance")]
    public int Balance { get; set; }
}