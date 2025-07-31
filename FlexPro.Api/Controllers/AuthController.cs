using FlexPro.Api.Application.Commands.Auth;
using FlexPro.Application.DTOs.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var token = await _mediator.Send(command);
            return token != null ? Ok(new {token}) : NotFound("Usuário ou senha incorretos");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var token = await _mediator.Send(new RegisterCommand { Register = dto});
            return token != null ? Ok(new {token}) : NotFound("Credenciais incorretas");
        }

        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRole([FromBody] UpdateUserRoleDto dto)
        {
            var result = await _mediator.Send(new UpdateUserRoleCommand(dto));
            return result ? Ok("Role adicionada com sucesso") : BadRequest("Falha ao adicionar role");
        }

        [HttpPost("get-roles")]
        public async Task<IActionResult> GetRoles([FromBody] CheckRoleDto dto)
        {
            var roles = await _mediator.Send(new CheckUserRoleCommand(dto));
            return Ok(new { roles });
        }
    }
}
