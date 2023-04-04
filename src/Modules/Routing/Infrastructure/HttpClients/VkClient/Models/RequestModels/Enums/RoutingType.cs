using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Ark.Routing.HttpClients.VkClient.Models.RequestModels.Enums;

/// <summary>
///     Тип точки маршрута
///     Типы для первой и последней точек маршрута игнорируются и всегда считаются равным break.
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum RoutingType
{
    /// <summary>
    ///     По умолчанию
    ///     Развороты разрешены, для этой точки будет создана отдельная ветка ведения в списке legs;
    /// </summary>
    [EnumMember(Value = "break")] Break,

    /// <summary>
    ///     Развороты запрещены, для этой точки не будет создана отдельная ветка ведения в списке legs
    /// </summary>
    [EnumMember(Value = "through")] Through,

    /// <summary>
    ///     Развороты разрешены, для этой точки не будет создана отдельная ветка ведения в списке legs
    /// </summary>
    [EnumMember(Value = "via")] Via,

    /// <summary>
    ///     Развороты разрешены, для этой точки будет создана отдельная ветка ведения в списке legs
    /// </summary>
    [EnumMember(Value = "break_through")] BreakThrough,
}