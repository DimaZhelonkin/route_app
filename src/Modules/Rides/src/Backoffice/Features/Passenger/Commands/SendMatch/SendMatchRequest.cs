using Ark.Core.Models;
using Ark.SharedLib.Common.CQS.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace Ark.Rides.Backoffice.Features.Passenger.Commands.SendMatch;

/// <summary>
///     Получить информацию для карточки поездки
/// </summary>
public record SendMatchRequest : CommandResult
{
    /// <summary>
    ///     Driver ride id
    /// </summary>
    [FromRoute]
    public Guid DriverRideId { get; set; }

    /// <summary>
    ///     Откуда начать поездку
    /// </summary>
    public Point StartPoint { get; set; }

    /// <summary>
    ///     Конечная точка поездки
    /// </summary>
    public Point Destination { get; set; }
}