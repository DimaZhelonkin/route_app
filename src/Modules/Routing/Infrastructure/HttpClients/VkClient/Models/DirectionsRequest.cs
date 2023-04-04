using Ark.Routing.Features.GetRouteQuery;
using Ark.Routing.HttpClients.VkClient.Models.RequestModels;
using Ark.Routing.HttpClients.VkClient.Models.RequestModels.Enums;
using Ark.Routing.Services;
using Ark.Routing.Services.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Ark.Routing.HttpClients.VkClient.Models;

public class DirectionsRequest
{
    public DirectionsRequest() { }

    public DirectionsRequest(GetRouteQuery query,List<Point> locations,  IOptions<RoutingOptions> options)
    {
        
        Id = query.Id;
        Locations = locations;
        Costing = (CostingTransportType) query.Costing;
        Units = options.Value.Units;
        Language = options.Value.Language;
        DirectionsType = options.Value.DirectionsType;
        Completeness = options.Value.Completeness;
        CostingOptions = options.Value.CostingOptions;
        DateTime = new()
        {
            new()
            {
                Type = (RoutingDateTimeType)query.DateTimeType,
            }
        };
        
    }
    
    /// <summary>
    ///     Список точек для построения маршрута в массиве JSON. Маршрут между точками строится в порядке, которые задан в
    ///     запросе.
    /// </summary>
    /// <example>"locations":[{"lat":55.796932,"lon":37.537849,"heading":150},{"lat":55.865625,"lon":37.462290,"type":"via"},{"lat":55.962139,"lon":37.406377}]</example>
    [JsonProperty("locations")]
    public List<Point> Locations { get; set; }

    /// <summary>
    ///     Тип транспорта для построения маршрута
    ///     По умолчанию: CostingTransportType.Auto
    /// </summary>
    /// <example>"costing":"pedestrian"</example>
    [JsonProperty("costing")]
    public CostingTransportType? Costing { get; set; }

    /// <summary>
    ///     Список параметров расчёта маршрута.
    ///     Для различных типов транспорта используются различные опции и ограничения.
    ///     При использовании параметров расчёта маршрута необходимо указать тип транспорта
    /// </summary>
    /// <example>
    ///     "costing_options": {"auto":{"use_tolls":0, "use_border_crossing":0}}
    /// </example>
    [JsonProperty("costing_options")]
    public Dictionary<CostingTransportType, CostingOptions>? CostingOptions { get; set; }

    /// <summary>
    ///     Единица измерения расстояния
    ///     По умолчанию:  DistanceUnits.Kilometers
    /// </summary>
    /// <example>"costing":"pedestrian"</example>
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
    ///     Идентификатор запроса, который возвращается вместе с ответом, что позволяет точно установить соответствие запроса и
    ///     ответа.
    /// </summary>
    /// <example>"id":"route_to_airport"</example>
    [JsonProperty("id")]
    public string? Id { get; set; }

    /// <summary>
    ///     Включить описание маневров.
    ///     По умолчанию:  DirectionsType.None
    /// </summary>
    /// <example>"directions_type":"instructions"</example>
    [JsonProperty("directions_type")]
    public DirectionsType? DirectionsType { get; set; }

    /// <summary>
    ///     Набор местоположений, которые необходимо избегать при построении маршрута.
    ///     Задаётся в виде массива точек с указанием широты и долготы
    /// </summary>
    /// <example>
    ///     "avoid_locations":[{"lat":55.8718,"lon":37.4577},{"lat":55.8845,"lon":37.4416},{"lat":55.9239,"lon":37.3951}]
    /// </example>
    [JsonProperty("avoid_locations")]
    public List<Point>? AvoidLocations { get; set; }

    /// <summary>
    ///     Дата и время в точке отправления или назначения для определения более точных результатов построения маршрута.
    /// </summary>
    /// <example>
    ///     "date_time":{"type":2,"value":"2020-12-33T21:00"}
    /// </example>
    [JsonProperty("date_time")]
    public List<RoutingDateTime>? DateTime { get; set; }

    /// <summary>
    ///     Максимальное количество альтернативных маршрутов в дополнение к основному (возвращается при их наличии).
    ///     Определяется значением от 0 до 4, где:
    ///     0 (по умолчанию) — будет построен один маршрут, без расчёта альтернативных;
    ///     4 — будет рассчитано пять маршрутов;
    ///     Дробные значения не допускаются.
    ///     TODO add validation
    /// </summary>
    /// <example>"alternates":3 </example>
    [JsonProperty("alternates")]
    public uint? Alternates { get; set; }

    /// <summary>
    ///     Атрибут для включения прокладки альтернативных маршрутов при наличии в запросе промежуточных точек.
    ///     По умолчанию — false.
    /// </summary>
    /// <example>"alternates_multi_points":true</example>
    [JsonProperty("alternates_multi_points")]
    public bool? AlternatesMultiPoints { get; set; }

    /// <summary>
    ///     Атрибут для расширения ответа.
    ///     minimal (по умолчанию)
    /// </summary>
    /// <example>"completeness":"enriched"</example>
    [JsonProperty("completeness")]
    public ResponseCompleteness? Completeness { get; set; }
}