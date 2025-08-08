using FlexPro.Application.DTOs.Auth;
using MediatR;

namespace FlexPro.Application.UseCases.Auth;

public sealed record AuthenticateLoginCommand(LoginRequest LoginRequest) : IRequest<LoginResponse?>;