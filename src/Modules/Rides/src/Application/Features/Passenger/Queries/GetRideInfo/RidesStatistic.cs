namespace Ark.Rides.Application.Features.Passenger.Queries.GetRideInfo;

public class RidesStatistic
{
    public decimal Rating { get; set; }
    public uint TotalRidesCount { get; set; }
    public uint FailedRidesCount { get; set; }

    public uint NumberOfReviews { get; set; }
}