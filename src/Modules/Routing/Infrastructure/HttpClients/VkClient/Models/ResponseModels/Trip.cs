using Newtonsoft.Json;

namespace Ark.Routing.HttpClients.VkClient.Models.ResponseModels;

// [JsonObject(MemberSerialization.OptIn)]
// [JsonArray]
public class Trip
{
    /// <summary>
    ///     Список точек маршрута в соответствии с запросом с дополнительной информацией об этих точках.
    /// </summary>
    /// <example>
    ///     "locations": [
    ///     {
    ///     "type": "break",
    ///     "lat": 55.796932,
    ///     "lon": 37.537849,
    ///     "heading": 150,
    ///     "side_of_street": "left",
    ///     "original_index": 0
    ///     },
    ///     {
    ///     "type": "via",
    ///     "lat": 55.865625,
    ///     "lon": 37.46229,
    ///     "original_index": 1
    ///     }
    ///     ]
    /// </example>
    [JsonProperty("locations")]
    public List<Location> Locations { get; set; }

    /// <summary>
    ///     Объект legs описывает маршрут и манёвры заключённые между парой точек маршрута, имеющих свойство "type":"break".
    ///     Для n точек маршрута типа break ответ содержит n-1 элемент описания маршрута (для запроса без альтернатив). Для
    ///     запроса с альтернативами каждая пара break-точек может содержать количество legs меньше или равное числу
    ///     альтернатив. Каждый объект описания маршрута содержит:
    ///     - словарь summary аналогичный общему, но относящийся только к его части между двумя точками маршрута типа break;
    ///     - строку shape;
    ///     - список maneuvers.
    /// </summary>
    [JsonProperty("legs")]
    public List<Leg> Legs { get; set; }

    /// <summary>
    ///     Общая информация о маршруте.
    /// </summary>
    [JsonProperty("summary")]
    public Summary Summary { get; set; }

    /// <summary>
    ///     Время построения маршрута
    /// </summary>
    [JsonProperty("build_route_time_ms")]
    public float BuildRouteTimeMs { get; set; }

    [JsonProperty("considered_edges")]
    public int ConsideredEdges { get; set; }
}