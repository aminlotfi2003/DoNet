using DoNet.Application.Abstractions.Identity;
using DoNet.Application.Abstractions.Services;
using MediatR;

namespace DoNet.Application.Identity.Commands.LogoutUser;

internal sealed class LogoutUserCommandHandler : IRequestHandler<LogoutUserCommand>
{
    private readonly IRefreshTokenRepository _refreshTokens;
    private readonly ITokenService _tokenService;
    private readonly IDateTimeProvider _clock;

    public LogoutUserCommandHandler(
        IRefreshTokenRepository refreshTokens,
        ITokenService tokenService,
        IDateTimeProvider clock)
    {
        _refreshTokens = refreshTokens;
        _tokenService = tokenService;
        _clock = clock;
    }

    public async Task Handle(LogoutUserCommand request, CancellationToken cancellationToken)
    {
        var hashedToken = _tokenService.ComputeHash(request.RefreshToken);
        var storedToken = await _refreshTokens.GetByTokenHashAsync(hashedToken, cancellationToken);

        if (storedToken is null)
            return;

        if (storedToken.IsActive(_clock.UtcNow))
            storedToken.Revoke();
            await _refreshTokens.SaveChangesAsync(cancellationToken);
    }
}
