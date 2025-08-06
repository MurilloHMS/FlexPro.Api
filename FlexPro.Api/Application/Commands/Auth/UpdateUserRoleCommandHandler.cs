using FlexPro.Domain.Entities;
using FlexPro.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FlexPro.Api.Application.Commands.Auth;

public class UpdateUserRoleCommandHandler : IRequestHandler<UpdateUserRoleCommand, bool>
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public UpdateUserRoleCommandHandler(UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<bool> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.Dto.Username);
        if (user == null) throw new Exception("User not found");

        if (!await _roleManager.RoleExistsAsync(request.Dto.Role))
            await _roleManager.CreateAsync(new IdentityRole(request.Dto.Role));

        var result = await _userManager.AddToRoleAsync(user, request.Dto.Role);
        return result.Succeeded;
    }
}