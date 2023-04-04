namespace Ark.Rides.Application.Models;

public class RideTime
{
    public DateTimeOffset Start { get; set; }
    public TimeSpan Duration { get; set; }
}