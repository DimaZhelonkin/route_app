using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Ark.Routing.Models.Enums;

/// <summary>
///     Тип транспорта для построения маршрута
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum CostingTransportType
{
    /// <summary>
    ///     автомобильный
    /// </summary>
    [EnumMember(Value = "auto")] Auto,

    /// <summary>
    ///     грузовой
    /// </summary>
    [EnumMember(Value = "truck")] Truck,

    /// <summary>
    ///     пешеходный
    /// </summary>
    [EnumMember(Value = "pedestrian")] Pedestrian,

    /// <summary>
    ///     велосипедный
    /// </summary>
    [EnumMember(Value = "bicycle")] Bicycle,

    /// <summary>
    ///     такси и другой транспорт допушенный к использованию полос общественного транспорта.
    /// </summary>
    [EnumMember(Value = "taxi")] Taxi
}