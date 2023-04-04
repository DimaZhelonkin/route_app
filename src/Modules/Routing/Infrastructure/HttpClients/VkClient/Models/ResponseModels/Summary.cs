using Newtonsoft.Json;

namespace Ark.Routing.HttpClients.VkClient.Models.ResponseModels;

/// <summary>
///     Общая информация о маршруте.
/// </summary>
public class Summary
{
    /// <summary>
    ///     TODO наверное это стоимость
    /// </summary>
    [JsonProperty("cost")]
    public float? Cost { get; set; }

    /// <summary>
    ///     Идентификатор запроса, который возвращается вместе с ответом, что позволяет точно установить соответствие запроса и
    ///     ответа.
    /// </summary>
    [JsonProperty("ll_boxes")]
    public List<LlBox> LlBoxes { get; set; }

    /// <summary>
    ///     Время пути
    /// </summary>
    /// <example>1733.264</example>
    [JsonProperty("time")]
    public float Time { get; set; }

    /// <summary>
    ///     Общая длина манёвра
    /// </summary>
    /// <example>26.588</example>
    [JsonProperty("length")]
    public float Length { get; set; }
}