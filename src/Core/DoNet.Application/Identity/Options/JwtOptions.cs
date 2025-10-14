namespace DoNet.Application.Identity.Options;

public sealed class JwtOptions
{
    public const string SectionName = "Jwt";

    public string Issuer { get; set; } = "DoNet";
    public string Audience { get; set; } = "DoNet";
    public string SigningKey { get; set; } = null!;
    public int AccessTokenLifetimeMinutes { get; set; } = 15;
    public int RefreshTokenLifetimeDays { get; set; } = 7;
}
