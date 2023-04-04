using Ark.Core.Models;
using Ark.SharedLib.Common.CQS.Implementations;

namespace Ark.Rides.Backoffice.Features.Driver.Commands.CreateDriverRide;

/// <summary>
/// </summary>
public record CreateDriverRideRequest : CommandResult
{
    /// <summary>
    ///     Откуда начать поездку
    /// </summary>
    public Point StartPoint { get; set; }

    /// <summary>
    ///     Конечная точка поездки
    /// </summary>
    public Point Destination { get; set; }

    /// <summary>
    ///     Дата начала поездки
    ///     Default: Now
    /// </summary>
    public DateTimeOffset? StartDate { get; set; } = DateTimeOffset.UtcNow;

    /// <summary>
    ///     Минимальная сумма поездки
    /// </summary>
    public decimal? MinPrice { get; set; }

    /// <summary>
    ///     Количество свободных мест на время поездки
    /// </summary>
    public uint Capacity { get; set; }

    /// <summary>
    ///     Выбранная машина для поездки
    ///     По умолчанию: выбранная машина в профиле
    /// </summary>
    public Guid? Vehicle { get; set; }

    /// <summary>
    /// На сколько сильно должен совпасть маршрут (в процентах)
    /// TODO Возможно нужен?
    /// </summary>
    // public decimal MatchPercent { get; set; }
}