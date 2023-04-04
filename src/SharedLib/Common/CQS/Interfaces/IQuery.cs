using Ark.SharedLib.Common.Results;
using MediatR;

namespace Ark.SharedLib.Common.CQS.Interfaces;

public interface IQuery<out TResponse> : IRequest<TResponse>
    where TResponse : Result
{
}