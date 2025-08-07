using FlexPro.Application.DTOs.Auth;
using MediatR;

namespace FlexPro.Application.UseCases.Users.Update;

public sealed record UpdateUserCommand(UserDto Dto) : IRequest<string?>;