using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Ark.Routing.HttpClients.VkClient.Models.RequestModels.Enums;

/// <summary>
///     Атрибут для расширения ответа
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum ResponseCompleteness
{
    /// <summary>
    ///     (по умолчанию) — возврат минимальной информации (только маневры, без ребер)
    /// </summary>
    [EnumMember(Value = "minimal")] Minimal,

    /// <summary>
    ///     возврат информации обо всех ребрах, входящих в маршрут
    /// </summary>
    [EnumMember(Value = "enriched")] Enriched,
}