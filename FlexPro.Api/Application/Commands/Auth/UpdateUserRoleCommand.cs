using FlexPro.Application.DTOs.Auth;
using MediatR;

namespace FlexPro.Api.Application.Commands.Auth
{
    public class UpdateUserRoleCommand : IRequest<bool>
    {
        public UpdateUserRoleDto Dto { get; set; }

        public UpdateUserRoleCommand(UpdateUserRoleDto dto)
        {
            Dto = dto;
        }
    }
}