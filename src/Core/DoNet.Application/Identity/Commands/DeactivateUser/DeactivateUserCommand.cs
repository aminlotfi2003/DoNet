using DoNet.Application.Identity.DTOs;
using MediatR;

namespace DoNet.Application.Identity.Commands.DeactivateUser;

public sealed record DeactivateUserCommand(Guid UserId) : IRequest<ApplicationUserDto>;
