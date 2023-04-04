using Ark.Routing.HttpClients.VkClient.Models.RequestModels.Enums;
using Ark.Routing.HttpClients.VkClient.Models.ResponseModels;
using Newtonsoft.Json;

namespace Ark.Routing.HttpClients.VkClient.Models;

public class DirectionsResponse
{
    /// <summary>
    ///     Описание ошибки.
    /// </summary>
    [JsonProperty("status_message")]
    public string StatusMessage { get; set; }

    /// <summary>
    ///     Код ошибки.
    /// </summary>
    [JsonProperty("status")]
    public int Status { get; set; }

    /// <summary>
    ///     Название ошибки
    /// </summary>
    [JsonProperty("error")]
    public string? Error { get; set; }

    /// <summary>
    ///     Код ошибки.
    /// </summary>
    [JsonProperty("status_code")]
    public int? StatusCode { get; set; }

    /// <summary>
    ///     Корневой список возвращаемых маршрутов.
    ///     Включает несколько элементов при запросе альтернативных маршрутов.
    /// </summary>
    [JsonProperty("trips")]
    
    public List<Trip?> Trips { get; set; }

    /// <summary>
    ///     Словарь, с полным описанием маршрута.
    /// </summary>
    // [JsonProperty("trip")]
    // public Dictionary<string, string> Trip { get; set; }

    /// <summary>
    ///     Единица измерения расстояния
    ///     По умолчанию:  DistanceUnits.Kilometers
    /// </summary>
    [JsonProperty("units")]
    public DistanceUnits? Units { get; set; }

    /// <summary>
    ///     Язык ответа
    ///     По умолчанию:  Language.Russian
    /// </summary>
    /// <example>"language":"en-US"</example>
    [JsonProperty("language")]
    public Language? Language { get; set; }

    /// <summary>
    ///     Список точек для построения маршрута в массиве JSON. Маршрут между точками строится в порядке, которые задан в
    ///     запросе.
    /// </summary>
    /// <example>"locations":[{"lat":55.796932,"lon":37.537849,"heading":150},{"lat":55.865625,"lon":37.462290,"type":"via"},{"lat":55.962139,"lon":37.406377}]</example>
    // [JsonProperty("locations")]
    // public List<Point> Locations { get; set; }


    /// <summary>
    ///     Идентификатор запроса, который возвращается вместе с ответом, что позволяет точно установить соответствие запроса и
    ///     ответа.
    /// </summary>
    /// <example>"id":"route_to_airport"</example>
    [JsonProperty("id")]
    public string? Id { get; set; }
}