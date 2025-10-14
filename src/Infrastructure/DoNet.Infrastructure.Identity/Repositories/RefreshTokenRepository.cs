using DoNet.Application.Abstractions.Identity;
using DoNet.Domain.Identity;
using DoNet.Infrastructure.Identity.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DoNet.Infrastructure.Identity.Repositories;

public sealed class RefreshTokenRepository(
    ApplicationIdentityDbContext context) : IRefreshTokenRepository
{
    private readonly ApplicationIdentityDbContext _context = context;

    public async Task AddAsync(RefreshToken token, CancellationToken cancellationToken = default)
    {
        await _context.RefreshTokens.AddAsync(token, cancellationToken);
    }

    public async Task<RefreshToken?> GetByTokenHashAsync(string tokenHash, CancellationToken cancellationToken = default)
    {
        return await _context.RefreshTokens
            .Include(t => t.User)
            .SingleOrDefaultAsync(t => t.TokenHash == tokenHash, cancellationToken);
    }

    public async Task RevokeUserTokensAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var tokens = await _context.RefreshTokens
            .Where(t => t.UserId == userId && !t.Revoked)
            .ToListAsync(cancellationToken);

        foreach (var token in tokens)
        {
            token.Revoke();
        }
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        => _context.SaveChangesAsync(cancellationToken);
}
