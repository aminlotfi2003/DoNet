using DoNet.Application.Identity.DTOs;
using MediatR;

namespace DoNet.Application.Identity.Commands.ForgotPassword;

public sealed record ForgotPasswordCommand(string Email) : IRequest<ForgotPasswordTokenDto>;
