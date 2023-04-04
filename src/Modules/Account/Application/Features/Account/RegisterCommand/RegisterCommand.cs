using Ark.SharedLib.Common.CQS.Implementations;

namespace Ark.Account.Features.Account.RegisterCommand;

public record RegisterCommand : CommandResult
{
    /// <summary>
    ///     Имя пользователя
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    ///     Фамилия пользователя
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    ///     Почта
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    ///     Дата рождения
    /// </summary>
    public DateOnly DateOfBirth { get; set; }

    /// <summary>
    ///     Пароль
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    ///     Подтверждение пароля
    /// </summary>
    public string ConfirmPassword { get; set; }

    /// <summary>
    ///     Промокод (необязательное поле)
    ///     TODO не нужно в первом релизе
    /// </summary>
    public string? PromotionCode { get; set; }
}