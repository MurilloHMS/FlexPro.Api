using FlexPro.Application.DTOs.Auth;
using MediatR;

namespace FlexPro.Application.UseCases.Users.CheckUserRole;

public sealed record CheckUserRoleCommand(CheckRoleDto Dto) : IRequest<List<string>>;