using FlexPro.Application.DTOs.Auth;
using MediatR;

namespace FlexPro.Api.Application.Commands.Auth;

public class UpdateUserRoleCommand : IRequest<bool>
{
    public UpdateUserRoleCommand(UpdateUserRoleDto dto)
    {
        Dto = dto;
    }

    public UpdateUserRoleDto Dto { get; set; }
}