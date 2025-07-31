using FlexPro.Application.DTOs.Auth;
using MediatR;

namespace FlexPro.Api.Application.Commands.Auth
{
    public class CheckUserRoleCommand : IRequest<List<string>>
    {
        public CheckRoleDto Dto { get; set; }

        public CheckUserRoleCommand(CheckRoleDto dto)
        {
            Dto = dto;
        }
    }
}