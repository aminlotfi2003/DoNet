using DoNet.Application.Identity.DTOs;
using DoNet.Domain.Identity;
using MediatR;

namespace DoNet.Application.Identity.Commands.RegisterUser;

public sealed record RegisterUserCommand(
    string Email,
    string Password,
    string ConfirmPassword,
    string FirstName,
    string LastName,
    Gender Gender,
    DateTimeOffset BirthDate
) : IRequest<AuthenticationResultDto>;
