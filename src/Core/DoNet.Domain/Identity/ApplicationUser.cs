using Microsoft.AspNetCore.Identity;

namespace DoNet.Domain.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public Gender? Gender { get; set; }
    public DateTimeOffset? BirthDate { get; set; }
    public bool IsActived { get; set; }
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new HashSet<RefreshToken>();
}

public enum Gender { Male, Female }
