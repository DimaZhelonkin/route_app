namespace Ark.Core.Models;

public class Point
{
    public Point(double lat, double lon)
    {
        Lat = lat;
        Lon = lon;
    }

    public double Lat { get; init; }
    public double Lon { get; init; }
}