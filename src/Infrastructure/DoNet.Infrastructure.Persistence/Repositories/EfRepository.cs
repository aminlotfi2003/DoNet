using DoNet.Application.Abstractions.Repositories;
using DoNet.Infrastructure.Persistence.Contexts;
using DoNet.SharedKernel.Abstractions.Domain;
using DoNet.SharedKernel.Contracts.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DoNet.Infrastructure.Persistence.Repositories;

public class EfRepository<TEntity> : IRepository<TEntity>
    where TEntity : EntityBase
{
    protected readonly ApplicationDbContext _db;
    protected readonly DbSet<TEntity> _set;

    public EfRepository(ApplicationDbContext db)
    {
        _db = db;
        _set = db.Set<TEntity>();
    }

    public virtual IQueryable<TEntity> Query() => _set.AsQueryable();

    public virtual async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await _set.FindAsync([id], ct);

    public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default)
        => await _set.AnyAsync(predicate, ct);

    public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken ct = default)
        => predicate is null ? await _set.CountAsync(ct) : await _set.CountAsync(predicate, ct);

    public virtual Task<List<TEntity>> ListAsync(CancellationToken ct = default)
        => _set.ToListAsync(ct);

    public virtual Task<List<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default)
    => _set.Where(predicate).ToListAsync(ct);

    public async Task<PagedResponse<TEntity>> GetPagedAsync(PagedRequest request, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        IQueryable<TEntity> query = _set.AsNoTracking();

        query = query.ApplySearching(request);

        var totalCount = await query.CountAsync(ct);

        query = query.ApplySorting(request);

        var items = await query.ApplyPaging(request).ToListAsync(ct);

        int pageNumber = request.PageNumber > 0 ? request.PageNumber : 1;
        int pageSize = request.PageSize > 0 ? Math.Min(request.PageSize, 10000) : 10;

        return new PagedResponse<TEntity>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken ct = default)
    {
        await _set.AddAsync(entity, ct);
        return entity;
    }

    public virtual Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken ct = default)
        => _set.AddRangeAsync(entities, ct);

    public virtual void Update(TEntity entity) => _set.Update(entity);

    public virtual void Remove(TEntity entity) => _set.Remove(entity);
}
