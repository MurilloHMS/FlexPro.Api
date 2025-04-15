using MediatR;
using FlexPro.Api.Application.DTOs;

namespace FlexPro.Api.Application.Commands.Auth
{
    public class LoginCommand : IRequest<string>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
