using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Ark.Routing.HttpClients.VkClient.Models.ResponseModels.Enums;

/// <summary>
///     Тип маршрутизации
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum TravelType
{
    /// <summary>
    ///     используется для "travel_mode"="drive" и "costing"="auto"
    /// </summary>
    [EnumMember(Value = "car")] Car,

    /// <summary>
    ///     используется для "travel_mode"="drive" и "costing"="truck"
    /// </summary>
    [EnumMember(Value = "tractor_trailer")]
    TractorTrailer,

    /// <summary>
    ///     используется для "travel_mode"="pedestrian"
    /// </summary>
    [EnumMember(Value = "foot")] Foot,

    /// <summary>
    ///     используется для "travel_mode"="bicycle"
    /// </summary>
    [EnumMember(Value = "road")] Road,
}