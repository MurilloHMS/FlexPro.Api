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

        if (!await roleManager.RoleExistsAsync(request.Dto.Role))
            await roleManager.CreateAsync(new IdentityRole(request.Dto.Role));
        
        await userManager.AddToRoleAsync(user, request.Dto.Role);
        return await jwtTokenGenerator.GenerateToken(user);
    }
}