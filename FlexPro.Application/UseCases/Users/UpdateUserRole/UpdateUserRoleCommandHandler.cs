using FlexPro.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FlexPro.Application.UseCases.Users.UpdateUserRole;

public class UpdateUserRoleCommandHandler(UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole> roleManager) : IRequestHandler<UpdateUserRoleCommand, bool>
{
    public async Task<bool> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByNameAsync(request.Dto.Username);
        if (user == null) throw new Exception("User not found");

        if (!await roleManager.RoleExistsAsync(request.Dto.Role))
            await roleManager.CreateAsync(new IdentityRole(request.Dto.Role));

        var result = await userManager.AddToRoleAsync(user, request.Dto.Role);
        return result.Succeeded;
    }
}