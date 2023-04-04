using System.Data;

namespace Ark.SharedLib.Application.Abstractions.Repositories;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    IDbTransaction BeginTransaction();
    IDbTransaction BeginTransaction(IsolationLevel isolationLevel);
    Task<IDbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);

    Task<IDbTransaction> BeginTransactionAsync(IsolationLevel isolationLevel,
        CancellationToken cancellationToken = default);
}