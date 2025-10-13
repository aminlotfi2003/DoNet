using Microsoft.AspNetCore.Identity;

namespace DoNet.Domain.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new HashSet<RefreshToken>();
}
