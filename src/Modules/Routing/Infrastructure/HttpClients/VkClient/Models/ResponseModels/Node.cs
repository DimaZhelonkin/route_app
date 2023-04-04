using Newtonsoft.Json;

namespace Ark.Routing.HttpClients.VkClient.Models.ResponseModels;

public class Node
{
    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("camera_type")]
    public string? CameraType { get; set; }

    [JsonProperty("shape_index")]
    public uint ShapeIndex { get; set; }

    [JsonProperty("speed")]
    public uint? Speed { get; set; }
}