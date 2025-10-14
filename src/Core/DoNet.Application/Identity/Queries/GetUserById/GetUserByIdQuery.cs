using DoNet.Application.Identity.DTOs;
using MediatR;

namespace DoNet.Application.Identity.Queries.GetUserById;

public sealed record GetUserByIdQuery(Guid UserId) : IRequest<ApplicationUserDto?>;
