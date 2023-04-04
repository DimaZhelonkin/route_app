using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Ark.Routing.HttpClients.VkClient.Models.RequestModels.Enums;

[JsonConverter(typeof(StringEnumConverter))]
public enum Language
{
    /// <summary>
    ///     Русский
    /// </summary>
    [EnumMember(Value = "ru-RU")] Russian,

    /// <summary>
    ///     Английский
    /// </summary>
    [EnumMember(Value = "en-US")] English,
}