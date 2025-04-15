using FlexPro.Api.Application.DTOs.Auth;
using MediatR;

namespace FlexPro.Api.Application.Commands.Auth
{
    public class UpdateUserRoleCommand : IRequest<bool>
    {
        public UpdateUserRoleDTO Dto { get; set; }

        public UpdateUserRoleCommand(UpdateUserRoleDTO dto)
        {
            Dto = dto;
        }
    }
}
