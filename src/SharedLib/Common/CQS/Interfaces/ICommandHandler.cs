using Ark.SharedLib.Common.Results;

namespace Ark.SharedLib.Common.CQS.Interfaces;

public interface ICommandHandler<in TCommand> : ICommandHandler<TCommand, Result>
    where TCommand : class, ICommand<Result>
{
}

public interface ICommandHandler<in TCommand, TResponse> : IHandler<TCommand, TResponse>
    where TCommand : class, ICommand<TResponse>
    where TResponse : Result
{
}