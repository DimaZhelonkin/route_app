using Ark.SharedLib.Common.CQS.Interfaces;
using Ark.SharedLib.Common.Results;

namespace Ark.SharedLib.Common.CQS.Implementations;

public abstract class CommandHandler<TCommand> : RequestMessageHandler<TCommand, Result>,
    ICommandHandler<TCommand, Result>
    where TCommand : class, ICommand<Result>
{
    #region ICommandHandler<TCommand,Result> Members

    public override abstract Task<Result> Handle(TCommand command, CancellationToken cancellationToken = default);

    #endregion
}

public abstract class CommandHandler<TCommand, TData> : RequestMessageHandler<TCommand, Result<TData>>
    where TCommand : class, ICommand<Result<TData>>
    where TData : class
{
    public override abstract Task<Result<TData>>
        Handle(TCommand command, CancellationToken cancellationToken = default);
}

// public abstract class CommandPagedResultHandler<TCommand, TData> : CommandHandler<TCommand, PagedResult<TData>>
//     where TCommand : PagedCommand<TData>
//     where TData : class
// {
// }