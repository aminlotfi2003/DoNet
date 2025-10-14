using DoNet.Application.Identity.DTOs;
using MediatR;

namespace DoNet.Application.Identity.Commands.ChangePassword;

public sealed record ChangePasswordCommand(
    Guid UserId,
    string CurrentPassword,
    string NewPassword
) : IRequest<AuthenticationResultDto>;
