namespace Ark.IdentityServer.Infrastructure.UCaller.Models;

/// <summary>
///     Error code from API see https://developer.ucaller.ru>
/// </summary>
public enum ErrorCode
{
    /// <summary>
    ///     Ваш IP адрес заблокирован
    /// </summary>
    IPBlocked = 0,

    /// <summary>
    ///     Неверный запрос. Проверьте синтаксис запроса и список используемых параметров (его можно найти на странице с
    ///     описанием метода).
    /// </summary>
    BadRequest = 1,

    /// <summary>
    ///     Один из необходимых параметров был не передан или неверен. Проверьте список требуемых параметров и их формат на
    ///     странице с описанием метода.
    /// </summary>
    BadParameters = 2,

    /// <summary>
    ///     Неверный номер телефона
    /// </summary>
    IncorrectPhoneNumber = 3,

    /// <summary>
    ///     Работа вашего сервиса отключена в настройках
    /// </summary>
    ServiceDisabled = 4,

    /// <summary>
    ///     Возникла ошибка при инициализации авторизации
    /// </summary>
    AuthorizationFailed = 5,

    /// <summary>
    ///     Авторизация для этой страны запрещена настройками географии работы в личном кабинете
    /// </summary>
    AuthorizationBlockedForCountry = 9,

    /// <summary>
    ///     Этот id не существует или у вас нет к нему доступа
    /// </summary>
    IdNotFound = 10,

    /// <summary>
    ///     Авторизация не может быть бесплатно повторена, время истекло
    /// </summary>
    TimeExpired = 11,

    /// <summary>
    ///     Авторизация не может быть бесплатно повторена, лимит исчерпан
    /// </summary>
    LimitIsReached = 12,

    /// <summary>
    ///     Ошибочная попытка бесплатной инициализации повторной авторизации
    /// </summary>
    FreeInitAuthorizationFailed = 13,

    /// <summary>
    ///     Достигнут лимит в 4 исходящих звонка в минуту или 30 вызовов в день для одного номера
    /// </summary>
    AuthorizationCountLimit = 18,

    /// <summary>
    ///     Подождите 15 секунд перед повторной авторизации на тот же номер
    /// </summary>
    Wait15ForReinit = 19,

    /// <summary>
    ///     Авторизация голосом для этой страны не доступна
    /// </summary>
    VoiceAuthorisationNotAvailable = 20,

    /// <summary>
    ///     Превышен лимиты авторизации в час. Лимиты устанавливаются в настройках сервиса.
    /// </summary>
    HourlyLimit = 50,

    /// <summary>
    ///     Аутентификация не удалась. Убедитесь, что Вы используете верную схему aутентификации.
    /// </summary>
    AuthenticationFailed = 401,

    /// <summary>
    ///     Метод не поддерживается.
    /// </summary>
    MethodNotSupported = 405,

    /// <summary>
    ///     Слишком много запросов в секунду
    /// </summary>
    TooManyRequests = 429,

    /// <summary>
    ///     Произошла внутренняя ошибка сервера.
    ///     Попробуйте повторить запрос позже.
    /// </summary>
    InternalError = 500,

    /// <summary>
    ///     Ваш аккаунт заблокирован
    /// </summary>
    AccountBlocked = 1001,

    /// <summary>
    ///     Недостаточно средств на балансе аккаунта
    /// </summary>
    NegativeBalance = 1002,

    /// <summary>
    ///     С этого IP запрещено обращаться к API этого сервиса
    /// </summary>
    ServiceIPBlocked = 1003,

    /// <summary>
    ///     Сервис заархивирован
    /// </summary>
    ServiceArchived = 1004,

    /// <summary>
    ///     Требуется верификация номера телефона в личном кабинете
    /// </summary>
    VerificationRequired = 1005,
}