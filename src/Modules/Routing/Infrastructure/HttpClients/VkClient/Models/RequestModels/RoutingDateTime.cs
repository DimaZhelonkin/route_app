using Ark.Routing.HttpClients.VkClient.Models.RequestModels.Enums;
using Newtonsoft.Json;

namespace Ark.Routing.HttpClients.VkClient.Models.RequestModels;

/// <summary>
///     Дата и время в точке отправления или назначения
/// </summary>
public class RoutingDateTime
{
    /// <summary>
    ///     Тип даты и времени
    /// </summary>
    /// <example>"type":2</example>
    [JsonProperty("type")]
    public RoutingDateTimeType Type { get; set; }

    /// <summary>
    ///     значение требуемых даты и времени указываются в формате ISO 8601 (ГГГГ-ММ-ДДTчч:мм) в местном часовом поясе
    ///     отправления или прибытия в зависимости от параметра type
    ///     TODO check and validate it
    /// </summary>
    [JsonProperty("value")]
    public DateTime? Value { get; set; } // TODO maybe it has to be DateTimeOffset???
}