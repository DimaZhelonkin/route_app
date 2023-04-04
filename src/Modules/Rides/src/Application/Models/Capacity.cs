namespace Ark.Rides.Application.Models;

public class Capacity
{
    /// <summary>
    ///     Всего мест в машине
    /// </summary>
    public uint TotalCount { get; set; }

    /// <summary>
    ///     Свободно мест в машине
    /// </summary>
    public uint EmptySeats { get; set; }

    /// <summary>
    ///     Занято мест в машине
    /// </summary>
    public uint OccupiedSeats { get; set; }
}