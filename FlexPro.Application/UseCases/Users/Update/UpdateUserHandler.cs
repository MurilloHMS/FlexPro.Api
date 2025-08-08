using FlexPro.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FlexPro.Application.UseCases.Users.Update;

public class UpdateUserHandler(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager) : IRequestHandler<UpdateUserCommand, string?>
{
    public async Task<string?> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.Dto.Id);
        if (user == null)
            throw new Exception("User not found");
        
        
        user.Email = request.Dto.Email;
        user.UserName = request.Dto.Username;
        
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
            
            var currentRoles =  await userManager.GetRolesAsync(user);
            
            var rolesToRemove = currentRoles.Except(request.Dto.Roles);
            if(rolesToRemove.Any())
                await userManager.RemoveFromRolesAsync(user, rolesToRemove);
            
            var rolesToAdd = request.Dto.Roles.Except(currentRoles);
            if(rolesToAdd.Any())
                await userManager.AddToRolesAsync(user, rolesToAdd);
            
            return "Roles Atualizado com sucesso";
        }

        return null;
    }
}