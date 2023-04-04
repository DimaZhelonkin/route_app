using Newtonsoft.Json;

namespace Ark.Routing.HttpClients.VkClient.Models.ResponseModels;

public class Leg
{
    /// <summary>
    ///     Список ребер, входящих во все маневры (возвращается при наличии "completeness":"enriched" во входном запросе).
    /// </summary>
    [JsonProperty("nodes")]
    public List<Node> Nodes { get; set; }

    /// <summary>
    ///     Список манёвров на маршруте, с указанием точек манёвров на полилинии, а также длины, длительности манёвра и
    ///     подсказок к манёврам на выбранном языке
    /// </summary>
    [JsonProperty("maneuvers")]
    public List<Maneuver> Maneuvers { get; set; }

    /// <summary>
    ///     Общая информация об оттрезке.
    /// </summary>
    [JsonProperty("summary")]
    public Summary Summary { get; set; }

    /// <summary>
    ///     Кодированный формат полилинии для хранения серии координат широты и долготы в виде одной строки
    /// </summary>
    [JsonProperty("shape")]
    public string Shape { get; set; }
}