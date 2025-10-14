using MediatR;

namespace DoNet.Application.Identity.Commands.LogoutUser;

public sealed record LogoutUserCommand(string RefreshToken) : IRequest;
