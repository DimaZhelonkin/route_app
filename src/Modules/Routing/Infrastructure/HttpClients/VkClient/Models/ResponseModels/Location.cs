using Ark.Routing.HttpClients.VkClient.Models.RequestModels;
using Newtonsoft.Json;

namespace Ark.Routing.HttpClients.VkClient.Models.ResponseModels;

public class Location : Point
{
    /// <summary>
    ///     Указание порядкового номера точки маршрута.
    /// </summary>
    [JsonProperty("side_of_street")]
    public string? SideOfStreet { get; set; }

    [JsonProperty("lan")]
    public float Lan { get; set; }
    
    [JsonProperty("lon")]
    public float Lon { get; set; }
    
    [JsonProperty("type")]
    public string? Type { get; set; }
    /// <summary>
    ///     Указание порядкового номера точки маршрута.
    /// </summary>
    [JsonProperty("original_index")]
    public int OriginalIndex { get; set; }
}