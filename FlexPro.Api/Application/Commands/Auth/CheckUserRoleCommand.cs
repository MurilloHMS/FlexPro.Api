using FlexPro.Application.DTOs.Auth;
using MediatR;

namespace FlexPro.Api.Application.Commands.Auth;

public class CheckUserRoleCommand : IRequest<List<string>>
{
    public CheckUserRoleCommand(CheckRoleDto dto)
    {
        Dto = dto;
    }

    public CheckRoleDto Dto { get; set; }
}