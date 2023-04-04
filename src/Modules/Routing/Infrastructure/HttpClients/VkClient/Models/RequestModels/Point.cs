using Ark.Routing.HttpClients.VkClient.Models.RequestModels.Enums;
using Newtonsoft.Json;

namespace Ark.Routing.HttpClients.VkClient.Models.RequestModels;

public class Point
{
    /// <summary>
    ///     Тип точки маршрута
    ///     По умолчанию: RoutingType.Break
    /// </summary>
    /// <example>"type":"break"</example>
    [JsonProperty("type")]
    public RoutingType? Type { get; set; }

    /// <summary>
    ///     Широта точки маршрута в градусах. Используется 6 знаков после запятой
    /// </summary>
    /// <example>"lat":55.796932</example>
    [JsonProperty("lat")]
    public float Lat { get; set; }

    /// <summary>
    ///     Долгота точки маршрута в градусах. Используется 6 знаков после запятой.
    /// </summary>
    /// <example>"lon":37.5378492</example>
    [JsonProperty("lon")]
    public float Lon { get; set; }

    /// <summary>
    ///     Предпочтительное направление движения при старте.
    ///     Направление указывается в градусах с севера по часовой стрелке, где север — 0°, восток — 90°, юг — 180°, запад —
    ///     270°.
    /// </summary>
    /// <example>"heading":150</example>
    [JsonProperty("heading")]
    public float? Heading { get; set; }
}