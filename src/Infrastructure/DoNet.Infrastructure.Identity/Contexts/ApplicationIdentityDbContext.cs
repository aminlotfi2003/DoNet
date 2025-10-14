using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using DoNet.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using DoNet.Infrastructure.Identity.Extensions;

namespace DoNet.Infrastructure.Identity.Contexts;

public class ApplicationIdentityDbContext
    : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid,
                        IdentityUserClaim<Guid>, IdentityUserRole<Guid>,
                        IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>,
                        IdentityUserToken<Guid>>
{
    public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options) : base(options) { }

    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.MapIdentityTables();

        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationIdentityDbContext).Assembly);
    }
}
