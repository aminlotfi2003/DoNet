using DoNet.Application.Identity.DTOs;
using MediatR;

namespace DoNet.Application.Identity.Queries.ListUsers;

public sealed record ListUsersQuery(bool IncludeInactive = true) : IRequest<IReadOnlyCollection<ApplicationUserDto>>;
