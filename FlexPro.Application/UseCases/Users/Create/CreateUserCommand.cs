using FlexPro.Application.DTOs.Auth;
using MediatR;

namespace FlexPro.Application.UseCases.Users.Create;

public sealed record CreateUserCommand(RegisterDto Dto) : IRequest<string?>;