namespace Ark.Rides.Domain.Aggregates.RideParticipant;

/// <summary>
/// 
/// </summary>
public interface IRideParticipant : IUser
{
}

/// <summary>
/// 
/// </summary>
/// <typeparam name="TId"></typeparam>
public interface IRideParticipant<out TId> : IRideParticipant, IUser<TId>
{
}