using FlexPro.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FlexPro.Application.UseCases.Users.CheckUserRole;

public class CheckUserRoleCommandHandler(UserManager<ApplicationUser> userManager) : IRequestHandler<CheckUserRoleCommand, List<string>>
{
    public async Task<List<string>> Handle(CheckUserRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByNameAsync(request.Dto.Username);
        if (user == null) throw new Exception("User not found");

        var roles = await userManager.GetRolesAsync(user);
        return roles.ToList();
    }
}