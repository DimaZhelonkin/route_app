using Ark.SharedLib.Application.Abstractions.Repositories;
using Ark.SharedLib.Common.CQS.Implementations;
using Ark.SharedLib.Common.Results;
using MediatR;

namespace Ark.SharedLib.Application.Behaviours;

public class TransactionPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : Command<TResponse>
    where TResponse : Result
{
    private readonly IUnitOfWork _unitOfWork;

    public TransactionPipeline(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        using var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken);

        var response = await next();

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        tx.Commit();

        return response;
    }
}