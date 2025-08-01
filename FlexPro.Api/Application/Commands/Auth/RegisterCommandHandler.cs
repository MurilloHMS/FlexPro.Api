using FlexPro.Domain.Repositories;
using FlexPro.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FlexPro.Api.Application.Commands.Auth;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, string>
{
    private readonly IJwtTokenGenerator<ApplicationUser> _jwt;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;


    public RegisterCommandHandler(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
        IJwtTokenGenerator<ApplicationUser> jwt)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _jwt = jwt;
    }

    public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Register;

        var user = new ApplicationUser
        {
            UserName = dto.Username,
            Email = dto.Username
        };

        var result = await _userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new Exception($"Erro ao registrar usuário: {errors}");
        }

        if (!await _roleManager.RoleExistsAsync(dto.Role))
            await _roleManager.CreateAsync(new IdentityRole(dto.Role));

        await _userManager.AddToRoleAsync(user, dto.Role);

        return await _jwt.GenerateToken(user);
    }
}