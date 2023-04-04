using Ark.SharedLib.Common.Results;
using MediatR;

namespace Ark.SharedLib.Common.CQS.Interfaces;

public interface ICommand<out TResponse> : IRequest<TResponse>
    where TResponse : Result
{
}

public interface ICommand : ICommand<Result>
{
}