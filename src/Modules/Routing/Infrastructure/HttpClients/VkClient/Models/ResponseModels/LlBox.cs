using Newtonsoft.Json;

namespace Ark.Routing.HttpClients.VkClient.Models.ResponseModels;

public class LlBox
{
    /// <summary>
    ///     Минимальная широта границы области просмотра, включающей в себя маршрут.
    ///     Используется 6 знаков после запятой.
    /// </summary>
    /// <example>55.79378</example>
    [JsonProperty("min_lat")]
    public float MinLat { get; set; }

    /// <summary>
    ///     Минимальная долгота границы области просмотра, включающей в себя маршрут.
    ///     Используется 6 знаков после запятой.
    /// </summary>
    /// <example>37.39366</example>
    [JsonProperty("min_lon")]
    public float MinLon { get; set; }

    /// <summary>
    ///     Максимальная широта границы области просмотра, включающей в себя маршрут.
    ///     Используется 6 знаков после запятой.
    /// </summary>
    /// <example>55.962686</example>
    [JsonProperty("max_lat")]
    public float MaxLat { get; set; }

    /// <summary>
    ///     Максимальная долгота границы области просмотра, включающей в себя маршрут.
    ///     Используется 6 знаков после запятой.
    /// </summary>
    /// <example>37.546925</example>
    [JsonProperty("max_lon")]
    public float MaxLon { get; set; }
}