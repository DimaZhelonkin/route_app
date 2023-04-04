using System.Linq.Expressions;
using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Ark.SharedLib.Application.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ark.SharedLib.Persistence.Repositories;

public abstract class EFReadOnlyRepository<TEntity, TId, TDbContext> : IReadOnlyRepository<TEntity, TId>
    where TEntity : class, Domain.Interfaces.IEntity<TId>
    where TDbContext : DbContext, IUnitOfWork
{
    private readonly ISpecificationEvaluator _specificationEvaluator;

    protected EFReadOnlyRepository(TDbContext context, ISpecificationEvaluator? specificationEvaluator = null)
    {
        _specificationEvaluator = specificationEvaluator ?? SpecificationEvaluator.Default;
        Context = context;
    }

    internal DbContext Context { get; }

    #region IReadOnlyRepository<TEntity,TId> Members

    public IQueryable<TEntity> Items => Context.Set<TEntity>();

    public virtual IQueryable<TEntity> GetBy(ISpecification<TEntity> specification) =>
        ApplySpecification(specification);

    public virtual IQueryable<TEntity> GetBy(Expression<Func<TEntity, bool>> criteria) => GetItems().Where(criteria);

    public virtual Task<TEntity> GetByIdAsync(TId id,
        CancellationToken cancellationToken = default) =>
        GetItems().SingleAsync(i => i.Id!.Equals(id), cancellationToken);

    public virtual Task<List<TEntity>> ListAsync(CancellationToken cancellationToken = default) =>
        GetItems().ToListAsync(cancellationToken);

    public virtual Task<List<TEntity>> ListAsync(ISpecification<TEntity> specification,
        CancellationToken cancellationToken = default) =>
        ApplySpecification(specification).ToListAsync(cancellationToken);

    public virtual Task<List<TEntity>> ListAsync(Expression<Func<TEntity, bool>> criteria,
        CancellationToken cancellationToken = default) =>
        GetItems().Where(criteria).ToListAsync(cancellationToken);

    public virtual bool Any(ISpecification<TEntity> specification) => ApplySpecification(specification).Any();

    public virtual bool Any(Expression<Func<TEntity, bool>> criteria) => GetItems().Any(criteria);

    public virtual Task<bool> AnyAsync(ISpecification<TEntity> specification,
        CancellationToken cancellationToken = default) =>
        ApplySpecification(specification, true).AnyAsync(cancellationToken);

    public virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> criteria,
        CancellationToken cancellationToken = default) =>
        GetItems().AnyAsync(criteria, cancellationToken);


    public virtual Task<TEntity> FirstAsync(CancellationToken cancellationToken = default) =>
        GetItems().FirstAsync(cancellationToken);

    public virtual Task<TEntity> FirstAsync(ISpecification<TEntity> specification,
        CancellationToken cancellationToken = default) =>
        ApplySpecification(specification).FirstAsync(cancellationToken);

    public virtual Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> criteria,
        CancellationToken cancellationToken = default) =>
        GetItems().FirstAsync(criteria, cancellationToken);

    public TEntity? FirstOrDefault() => GetItems().FirstOrDefault();

    public virtual TEntity? FirstOrDefault(ISpecification<TEntity> specification) =>
        ApplySpecification(specification).FirstOrDefault();

    public virtual TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> criteria) =>
        GetItems().FirstOrDefault(criteria);

    public Task<TEntity?> FirstOrDefaultAsync(CancellationToken cancellationToken = default) =>
        GetItems().FirstOrDefaultAsync(cancellationToken);

    public virtual Task<TEntity?> FirstOrDefaultAsync(ISpecification<TEntity> specification,
        CancellationToken cancellationToken = default) =>
        ApplySpecification(specification).FirstOrDefaultAsync(cancellationToken);

    public virtual Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> criteria,
        CancellationToken cancellationToken = default) =>
        GetItems().FirstOrDefaultAsync(criteria, cancellationToken);

    public virtual TEntity Single(ISpecification<TEntity> specification) => ApplySpecification(specification).Single();

    public virtual TEntity Single(Expression<Func<TEntity, bool>> criteria) => GetItems().Single(criteria);

    public virtual Task<TEntity> SingleAsync(ISpecification<TEntity> specification,
        CancellationToken cancellationToken = default) =>
        ApplySpecification(specification).SingleAsync(cancellationToken);

    public virtual Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> criteria,
        CancellationToken cancellationToken = default) =>
        GetItems().SingleAsync(criteria, cancellationToken);

    public virtual int Count() => GetItems().Count();

    public virtual Task<int> CountAsync(CancellationToken cancellationToken = default) =>
        GetItems().CountAsync(cancellationToken);

    public virtual Task<int> CountAsync(ISpecification<TEntity> specification,
        CancellationToken cancellationToken = default) =>
        ApplySpecification(specification, true).CountAsync(cancellationToken);

    public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> criteria,
        CancellationToken cancellationToken = default) =>
        GetItems().CountAsync(criteria, cancellationToken);

    public virtual Task<long> LongCountAsync(CancellationToken cancellationToken = default) =>
        GetItems().LongCountAsync(cancellationToken);

    public virtual Task<long> LongCountAsync(ISpecification<TEntity> specification,
        CancellationToken cancellationToken = default) =>
        ApplySpecification(specification, true).LongCountAsync(cancellationToken);

    public virtual Task<long> LongCountAsync(Expression<Func<TEntity, bool>> criteria,
        CancellationToken cancellationToken = default) =>
        GetItems().LongCountAsync(criteria, cancellationToken);

    public virtual TEntity GetById(TId id) => GetItems().Single(i => i.Id != null && i.Id.Equals(id));

    public virtual long LongCount() => GetItems().LongCount();

    #endregion

    /// <summary>
    ///     Filters the entities  of <typeparamref name="TEntity" />, to those that match the encapsulated query logic of the
    ///     <paramref name="specification" />.
    /// </summary>
    /// <param name="specification">The encapsulated query logic.</param>
    /// <param name="evaluateCriteriaOnly"></param>
    /// <returns>The filtered entities as an <see cref="IQueryable{T}" />.</returns>
    protected virtual IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification,
        bool evaluateCriteriaOnly = false)
    {
        var inputQuery = GetItems();
        return _specificationEvaluator.GetQuery(inputQuery, specification, evaluateCriteriaOnly);
    }

    /// <summary>
    ///     Filters all entities of <typeparamref name="TEntity" />, that matches the encapsulated query logic of the
    ///     <paramref name="specification" />, from the database.
    ///     <para>
    ///         Projects each entity into a new form, being <typeparamref name="TResult" />.
    ///     </para>
    /// </summary>
    /// <typeparam name="TResult">The type of the value returned by the projection.</typeparam>
    /// <param name="specification">The encapsulated query logic.</param>
    /// <returns>The filtered projected entities as an <see cref="IQueryable{T}" />.</returns>
    protected virtual IQueryable<TResult> ApplySpecification<TResult>(ISpecification<TEntity, TResult> specification)
    {
        var inputQuery = GetItems();
        return _specificationEvaluator.GetQuery(inputQuery, specification);
    }

    protected virtual IQueryable<TEntity> GetItems() => Items;
}