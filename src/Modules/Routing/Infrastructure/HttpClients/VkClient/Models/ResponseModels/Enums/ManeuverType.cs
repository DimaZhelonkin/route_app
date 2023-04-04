namespace Ark.Routing.HttpClients.VkClient.Models.ResponseModels.Enums;

/// <summary>
///     Код типа манёвра
/// </summary>
public enum ManeuverType
{
    /// <summary>
    /// </summary>
    None,

    /// <summary>
    /// </summary>
    Start,

    /// <summary>
    /// </summary>
    StartRight,

    /// <summary>
    /// </summary>
    StartLeft,

    /// <summary>
    ///     В конце маршрута, когда прибыли в точку назначения.
    /// </summary>
    Destination,

    /// <summary>
    ///     В конце маршрута, когда точка назначения находится справа.
    /// </summary>
    DestinationRight,

    /// <summary>
    ///     В конце маршрута, когда точка назначения находится слева.
    /// </summary>
    DestinationLeft,

    /// <summary>
    /// </summary>
    Becomes,

    /// <summary>
    ///     Продолжайте движение (кажется, что аналог kStayStraight).
    /// </summary>
    Continue,

    /// <summary>
    ///     Плавный поворот направо.
    /// </summary>
    SlightRight,

    /// <summary>
    ///     Поворот направо.
    /// </summary>
    Right,

    /// <summary>
    ///     Резкий поворот направо.
    /// </summary>
    SharpRight,

    /// <summary>
    ///     Разворот направо (в основном на дорогах с левосторонним движением).
    /// </summary>
    UturnRight,

    /// <summary>
    ///     Разворот налево.
    /// </summary>
    UturnLeft,

    /// <summary>
    ///     Резкий поворот налево.
    /// </summary>
    SharpLeft,

    /// <summary>
    ///     Поворот налево.
    /// </summary>
    Left,

    /// <summary>
    ///     Плавный поворот налев.
    /// </summary>
    SlightLeft,

    /// <summary>
    ///     Продолжайте движение прямо.
    /// </summary>
    RampStraight,

    /// <summary>
    ///     Держитесь правее (например, на многополосной дороге).
    /// </summary>
    RampRight,

    /// <summary>
    ///     Держитесь левее (например, на многополосной дороге).
    /// </summary>
    RampLeft,

    /// <summary>
    ///     Съезд с магистрали направо.
    /// </summary>
    ExitRight,

    /// <summary>
    ///     Съезд с магистрали налево.
    /// </summary>
    ExitLeft,

    /// <summary>
    ///     Продолжайте движение прямо.
    /// </summary>
    StayStraight,

    /// <summary>
    ///     Держитесь правее (например, на многополосной дороге).
    /// </summary>
    StayRight,

    /// <summary>
    ///     Держитесь левее (например, на многополосной дороге).
    /// </summary>
    StayLeft,

    /// <summary>
    ///     Въезд на магистраль.
    /// </summary>
    Merge,

    /// <summary>
    ///     Въезд на кольцо. В этом сообщении приходит и номер съезда (от 1 до 9).
    /// </summary>
    RoundaboutEnter,

    /// <summary>
    ///     Съезд с кольца.
    /// </summary>
    RoundaboutExit,

    /// <summary>
    ///     Въезд на паром.
    /// </summary>
    FerryEnter,

    /// <summary>
    ///     Выезд с парома.
    /// </summary>
    FerryExit,

    /// <summary>
    /// </summary>
    Transit,

    /// <summary>
    /// </summary>
    TransitTransfer,

    /// <summary>
    /// </summary>
    TransitRemainOn,

    /// <summary>
    /// </summary>
    TransitConnectionStart,

    /// <summary>
    /// </summary>
    TransitConnectionTransfer,

    /// <summary>
    /// </summary>
    TransitConnectionDestination,

    /// <summary>
    /// </summary>
    PostTransitConnectionDestination,

    /// <summary>
    ///     Въезд на магистраль справа.
    /// </summary>
    MergeRight,

    /// <summary>
    ///     Въезд на магистраль слева.
    /// </summary>
    MergeLeft,
}