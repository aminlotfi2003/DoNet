using DoNet.Application.Identity.Models;

namespace DoNet.Application.Identity.DTOs;

public sealed record AuthenticationResultDto(
    string AccessToken,
    DateTimeOffset AccessTokenExpiresAt,
    string RefreshToken,
    DateTimeOffset RefreshTokenExpiresAt)
{
    public static AuthenticationResultDto FromTokenPair(TokenPair tokenPair) =>
        new(
            tokenPair.AccessToken,
            tokenPair.AccessTokenExpiresAt,
            tokenPair.RefreshToken,
            tokenPair.RefreshTokenExpiresAt
        );
}
