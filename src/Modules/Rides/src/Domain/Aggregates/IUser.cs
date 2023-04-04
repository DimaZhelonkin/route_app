using Ark.SharedLib.Domain.Interfaces;

namespace Ark.Rides.Domain.Aggregates;

/// <summary>
/// 
/// </summary>
public interface IUser : IAggregateRoot
{
}

public interface IUser<out TId> : IUser, IAggregateRoot<TId>
{
}