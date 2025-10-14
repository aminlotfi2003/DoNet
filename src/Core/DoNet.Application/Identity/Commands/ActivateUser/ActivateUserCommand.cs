using DoNet.Application.Identity.DTOs;
using MediatR;

namespace DoNet.Application.Identity.Commands.ActivateUser;

public sealed record ActivateUserCommand(Guid UserId) : IRequest<ApplicationUserDto>;
