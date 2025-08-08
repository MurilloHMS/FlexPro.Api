using FlexPro.Domain.Entities;
using FlexPro.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FlexPro.Application.UseCases.Users.Create;

public class CreateUserHandler(
    IJwtTokenGenerator<ApplicationUser> jwtTokenGenerator,
    RoleManager<IdentityRole> roleManager,
    UserManager<ApplicationUser> userManager) : IRequestHandler<CreateUserCommand, string?>
{
    public async Task<string?> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new ApplicationUser()
        {
            UserName = request.Dto.Username,
            Email = request.Dto.Email
        };
        
        var result = await userManager.CreateAsync(user, request.Dto.Password);
        
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new Exception($"Erro ao registrar usuário: {errors}");
        }

        if (request.Dto.Roles != null)
        {
            foreach (var role in request.Dto.Roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }
            await userManager.AddToRolesAsync(user, request.Dto.Roles);
        }
        return await jwtTokenGenerator.GenerateToken(user);
    }
}