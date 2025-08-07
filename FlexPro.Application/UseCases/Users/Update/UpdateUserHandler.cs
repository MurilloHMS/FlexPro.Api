using FlexPro.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FlexPro.Application.UseCases.Users.Update;

public class UpdateUserHandler(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager) : IRequestHandler<UpdateUserCommand, string?>
{
    public async Task<string?> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new ApplicationUser()
        {
            Id = request.Dto.Id,
            Email = request.Dto.Email,
            UserName = request.Dto.Username
        };
        var result = await userManager.UpdateAsync(user);
        
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new Exception($"Erro ao atualizar usuário: {errors}");
        }

        if (request.Dto.Roles != null)
        {
            foreach (var role in request.Dto.Roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }
            await userManager.AddToRolesAsync(user, request.Dto.Roles);
            return "Roles Atualizado com sucesso";
        }

        return null;
    }
}