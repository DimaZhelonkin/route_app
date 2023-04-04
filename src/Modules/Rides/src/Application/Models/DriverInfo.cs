using Ark.Rides.Application.Features.Passenger.Queries.GetRideInfo;

namespace Ark.Rides.Application.Models;

public class DriverInfo
{
    public string Name { get; set; }
    public string AvatarUrl { get; set; }
    public string Description { get; set; }
    public RidesStatistic Statistic { get; set; }

    public AboutMyself AboutMyself { get; set; }
}