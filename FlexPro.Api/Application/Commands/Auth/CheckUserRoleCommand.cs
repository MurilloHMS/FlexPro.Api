using FlexPro.Api.Application.DTOs.Auth;
using MediatR;

namespace FlexPro.Api.Application.Commands.Auth
{
    public class CheckUserRoleCommand : IRequest<List<string>>
    {
        public CheckRoleDTO Dto { get; set; }
        public CheckUserRoleCommand(CheckRoleDTO dto)
        {
            Dto = dto;
        }
    }
}
