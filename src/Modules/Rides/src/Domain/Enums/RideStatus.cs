namespace Ark.Rides.Domain.Enums;

[Flags]
public enum RideStatus
{
    Created,
    StartWaiting,
    Started,
    OnTheWay,
    Finished,
    Cancelled,
    Removed,
}