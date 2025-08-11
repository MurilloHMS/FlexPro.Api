using FlexPro.Application.DTOs.Auth;
using MediatR;

namespace FlexPro.Application.UseCases.Users.UpdateUserRole;

public sealed record UpdateUserRoleCommand(UpdateUserRoleDto Dto): IRequest<bool>;