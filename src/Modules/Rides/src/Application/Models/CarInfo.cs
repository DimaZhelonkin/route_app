namespace Ark.Rides.Application.Models;

public class CarInfo
{
    public string Name { get; set; }

    /// <summary>
    ///     Вместимость автомобиля
    /// </summary>
    public Capacity Capacity { get; set; }
}