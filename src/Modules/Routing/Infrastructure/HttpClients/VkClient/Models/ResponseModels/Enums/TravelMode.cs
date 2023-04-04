using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Ark.Routing.HttpClients.VkClient.Models.ResponseModels.Enums;

/// <summary>
///     Режим маршрутизации
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum TravelMode
{
    /// <summary>
    ///     используется для типов транспорта "costing"="auto" и "costing"="truck"
    /// </summary>
    [EnumMember(Value = "drive")] Drive,

    [EnumMember(Value = "pedestrian")] Pedestrian,

    [EnumMember(Value = "bicycle")] Bicycle,
}