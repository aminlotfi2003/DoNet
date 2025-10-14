using DoNet.Application.Identity.DTOs;
using MediatR;

namespace DoNet.Application.Identity.Commands.LoginUser;

public sealed record LoginUserCommand(string Email, string Password) : IRequest<AuthenticationResultDto>;
