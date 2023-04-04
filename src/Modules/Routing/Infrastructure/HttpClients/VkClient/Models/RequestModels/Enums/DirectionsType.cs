using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Ark.Routing.HttpClients.VkClient.Models.RequestModels.Enums;

[JsonConverter(typeof(StringEnumConverter))]
public enum DirectionsType
{
    /// <summary>
    ///     По умолчанию
    ///     Исключить описание манёвров из ответа
    /// </summary>
    [EnumMember(Value = "none")] None,

    /// <summary>
    ///     Включить описание манёвров в ответ
    /// </summary>
    [EnumMember(Value = "maneuvers")] Maneuvers,

    /// <summary>
    ///     Добавить в описание маневров инструкции на соответствующем языке.
    /// </summary>
    [EnumMember(Value = "instructions")] Instructions,
}