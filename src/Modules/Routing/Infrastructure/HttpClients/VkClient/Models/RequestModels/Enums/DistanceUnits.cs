using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Ark.Routing.HttpClients.VkClient.Models.RequestModels.Enums;

/// <summary>
///     Единица измерения расстояния
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum DistanceUnits
{
    /// <summary>
    ///     километры
    /// </summary>
    [EnumMember(Value = "kilometers")] Kilometers,

    /// <summary>
    ///     мили
    /// </summary>
    [EnumMember(Value = "miles")] Miles,
}