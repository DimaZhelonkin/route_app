using System.Collections.Concurrent;
using System.Data;
using System.Linq.Expressions;
using Ark.SharedLib.Application.Abstractions.Repositories;
using Ark.SharedLib.Domain.Interfaces;
using Ark.SharedLib.Domain.Models;
using Ark.SharedLib.Persistence.Audit;
using Ark.SharedLib.Persistence.Extensions;
using Ark.StronglyTypedIds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Ark.SharedLib.Persistence.DbContexts;

/// <summary>
/// </summary>
public abstract class BaseDbContext : DbContext, IUnitOfWork
{
    private IDbContextTransaction _transaction;

    /// <summary>
    /// </summary>
    /// <param name="options"></param>
    public BaseDbContext(DbContextOptions options) : base(options)
    {
    }

    /// <summary>
    /// </summary>
    public DbSet<AuditHistory> AuditHistory { get; set; }

    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    #region IUnitOfWork Members

    async Task<IDbTransaction> IUnitOfWork.BeginTransactionAsync(IsolationLevel isolationLevel,
        CancellationToken cancellationToken)
    {
        var transaction = await Database.BeginTransactionAsync(isolationLevel, cancellationToken);
        return transaction.GetDbTransaction();
    }

    async Task<IDbTransaction> IUnitOfWork.BeginTransactionAsync(CancellationToken cancellationToken)
    {
        var transaction = await Database.BeginTransactionAsync(cancellationToken);
        return transaction.GetDbTransaction();
    }


    IDbTransaction IUnitOfWork.BeginTransaction()
    {
        var transaction = Database.BeginTransaction();
        return transaction.GetDbTransaction();
    }

    IDbTransaction IUnitOfWork.BeginTransaction(IsolationLevel isolationLevel)
    {
        var transaction = Database.BeginTransaction(isolationLevel);
        return transaction.GetDbTransaction();
    }

    #endregion

    /// <summary>
    ///     Entity model definitions
    /// </summary>
    /// <param name="builder"></param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.EnableAuditHistory();
        var schema = GetType().Name.Replace("DbContext", "");
        builder.HasDefaultSchema(schema);

        // ExtractExternalEntities(builder);
        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        foreach (var mutableEntityType in builder.Model.GetEntityTypes())
            if (mutableEntityType.ClrType.IsAssignableTo(typeof(IAuditableEntity)))
                builder.Entity(mutableEntityType.ClrType)
                       .Property(nameof(IAuditableEntity.DeletedOnUtc))
                       .IsRequired(false);

        builder.AddStronglyTypedIdConversions();
    }
}