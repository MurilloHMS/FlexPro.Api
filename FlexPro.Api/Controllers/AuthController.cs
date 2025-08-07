using FlexPro.Api.Application.Commands.Auth;
using FlexPro.Application.DTOs.Auth;
using FlexPro.Application.UseCases.Auth;
using FlexPro.Application.UseCases.Users.Create;
using FlexPro.Application.UseCases.Users.GetAll;
using FlexPro.Application.UseCases.Users.Update;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlexPro.Api.Controllers;

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
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        var token = await _mediator.Send(new AuthenticateLoginCommand(loginRequest));
        return token != null 
            ? Ok(token) 
            : NotFound("Usuário ou senha incorretos");
    }

    [HttpPost("register")]
    public async Task<IResult> Register([FromBody] RegisterDto dto)
    {
        var token = await _mediator.Send(new CreateUserCommand(dto));
        return token != null 
            ? Results.Ok(new { token }) 
            : Results.NotFound("Credenciais incorretas");
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
    
    [HttpGet("get-all-users")]
    [Authorize(Roles = "Admin,Developer")]
    public async Task<IResult> GetUsers()
    {
        var result = await _mediator.Send(new GetAllUsersQuery());
        return result.IsSuccess
            ? Results.Ok(result.Value.UserResponse)
            : Results.NotFound(result.Error);
    }

    [HttpGet("update")]
    [Authorize(Roles = "Admin,Developer")]
    public async Task<IActionResult> UpdateRoles([FromBody] UserDto dto)
    {
        var result = await _mediator.Send(new UpdateUserCommand(dto));
        return result != null
            ? Ok(result)
            : BadRequest(result);
    }
}