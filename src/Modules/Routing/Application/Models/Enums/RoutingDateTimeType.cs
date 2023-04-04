using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Ark.Routing.Models.Enums;

[JsonConverter(typeof(StringEnumConverter))]
public enum RoutingDateTimeType
{
    /// <summary>
    ///     (по умолчанию) — текущая дата и время в точке отправления,
    ///     значение value игнорируется;
    /// </summary>
    Default,

    /// <summary>
    ///     дата и время отправления
    /// </summary>
    Departure,

    /// <summary>
    ///     дата и время прибытия
    /// </summary>
    Arrival
}