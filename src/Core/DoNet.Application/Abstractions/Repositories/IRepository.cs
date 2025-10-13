using System.Linq.Expressions;
using DoNet.SharedKernel.Abstractions.Domain;
using DoNet.SharedKernel.Contracts.Pagination;

namespace DoNet.Application.Abstractions.Repositories;

public interface IRepository<TEntity> where TEntity : EntityBase
{
    IQueryable<TEntity> Query();

    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default);
    Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken ct = default);

    Task<List<TEntity>> ListAsync(CancellationToken ct = default);
    Task<List<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default);
    Task<PagedResponse<TEntity>> GetPagedAsync(PagedRequest request, CancellationToken ct = default);

    Task<TEntity> AddAsync(TEntity entity, CancellationToken ct = default);
    Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken ct = default);
    void Update(TEntity entity);
    void Remove(TEntity entity);
}
