namespace DoNet.Application.Abstractions.UoW;

public interface IUnitOfWork : IAsyncDisposable
{
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
