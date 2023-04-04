using Ark.Routing.HttpClients.VkClient.Models.ResponseModels.Enums;
using Newtonsoft.Json;

namespace Ark.Routing.HttpClients.VkClient.Models.ResponseModels;

public class Maneuver
{
    /// <summary>
    ///     Код типа манёвра
    /// </summary>
    [JsonProperty("type")]
    public ManeuverType Type { get; set; }

    /// <summary>
    ///     Текст, который рекомендуется использовать для отображения текстовой подсказки о манёвре.
    /// </summary>
    [JsonProperty("instruction")]
    public string? Instruction { get; set; }

    /// <summary>
    ///     Краткий текст, который можно использовать в качестве устного сообщения для подготовки к манёвру.
    /// </summary>
    [JsonProperty("verbal_succinct_transition_instruction")]
    public string? VerbalSuccinctTransitionInstruction { get; set; }

    /// <summary>
    ///     Текст, который можно использовать в качестве устного сообщения о манёвре, непосредственно перед самим манёвром.
    /// </summary>
    [JsonProperty("verbal_transition_alert_instruction")]
    public string? VerbalTransitionAlertInstruction { get; set; }

    /// <summary>
    ///     Текст, который можно использовать в качестве устного сообщения для подготовки к манёвру.
    /// </summary>
    [JsonProperty("verbal_pre_transition_instruction")]
    public string? VerbalPreTransitionInstruction { get; set; }

    /// <summary>
    ///     Текст, который можно использовать в качестве устного сообщения непосредственно сразу после завершения манёвра.
    /// </summary>
    [JsonProperty("verbal_post_transition_instruction")]
    public string? VerbalPostTransitionInstruction { get; set; }

    /// <summary>
    ///     Список названий улиц манёвра.
    /// </summary>
    [JsonProperty("street_names")]
    public List<string>? StreetNames { get; set; }


    /// <summary>
    ///     Время пути
    /// </summary>
    /// <example>1733.264</example>
    [JsonProperty("time")]
    public float Time { get; set; }

    /// <summary>
    ///     Общая длина манёвра в границах между begin_shape_index и end_shape_index, указывается в выбранных единицах
    ///     измерения.
    /// </summary>
    /// <example>26.588</example>
    [JsonProperty("length")]
    public float Length { get; set; }

    /// <summary>
    ///     TODO наверное это стоимость
    /// </summary>
    [JsonProperty("cost")]
    public float? Cost { get; set; }

    /// <summary>
    ///     Указатель на точку начала манёвра на полилинии.
    /// </summary>
    [JsonProperty("begin_shape_index")]
    public int BeginShapeIndex { get; set; }

    /// <summary>
    ///     Указатель на точку окончания манёвра на полилинии.
    /// </summary>
    [JsonProperty("end_shape_index")]
    public int EndShapeIndex { get; set; }

    /// <summary>
    ///     Переменная имеет значение true в случаях, когда текст устного сообщения о подготовке к манёвру
    ///     verbal_pre_transition_instruction содержит указание о нескольких последовательных, близко расположенных манёврах.
    /// </summary>
    [JsonProperty("verbal_multi_cue")]
    public bool VerbalMultiCue { get; set; }

    /// <summary>
    ///     Порядковый номер съезда с кругового движения.
    /// </summary>
    [JsonProperty("roundabout_exit_count")]
    public uint RoundaboutExitCount { get; set; }

    /// <summary>
    ///     Переменная имеет значение true в случаях, когда манёвр или его часть подлежат оплате. Например: часть манёвра
    ///     пролегает по платной дороге.
    /// </summary>
    [JsonProperty("toll")]
    public bool Toll { get; set; }

    /// <summary>
    ///     Переменная имеет значение true в случаях, когда манёвр невозможен без использования парома.
    /// </summary>
    [JsonProperty("ferry")]
    public bool Ferry { get; set; }

    /// <summary>
    ///     Переменная имеет значение true в случаях, когда манёвр полностью либо частично осуществляется по дорогам без
    ///     покрытия.
    /// </summary>
    [JsonProperty("rough")]
    public bool Rough { get; set; }

    /// <summary>
    ///     Переменная имеет значение true в случаях, когда манёвр пересекает ворота/шлагбаумы.
    /// </summary>
    [JsonProperty("gate")]
    public bool Gate { get; set; }
    /// <summary>
    ///  ISO-код страны (атрибут ребра). Указывается в том, случае, когда ребро пограничное для маршрута, пересекающего границу. Формат ISO 3166-1 alpha-2 (двухбуквенный).
    /// </summary>
    // [JsonProperty("iso_code")]
    // public string? IsoCode { get; set; }

    /// <summary>
    ///     Режим маршрутизации
    /// </summary>
    [JsonProperty("travel_mode")]
    public TravelMode TravelMode { get; set; }

    /// <summary>
    ///     Тип маршрутизации
    /// </summary>
    [JsonProperty("travel_type")]
    public TravelType TravelType { get; set; }
}