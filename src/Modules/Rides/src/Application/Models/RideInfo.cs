namespace Ark.Rides.Application.Models;

public class RideInfo
{
    public Guid RideId { get; set; }
    public DriverInfo DriverInfo { get; set; }
    public decimal Price { get; set; }
    public RideTime RideTime { get; set; }
    public CarInfo Car { get; set; }
    public string DriverRoute { get; set; }

    /// <summary>
    ///     Итоговый маршрут пассажира
    ///     (example: пешеходный до места встречи, общий на машине, место высадки и пешком до конечной точки)
    /// </summary>
    public string FinalRoute { get; set; }
}