using DoNet.Application.Identity.DTOs;
using MediatR;

namespace DoNet.Application.Identity.Commands.RefreshToken;

public sealed record RefreshTokenCommand(string RefreshToken) : IRequest<AuthenticationResultDto>;
