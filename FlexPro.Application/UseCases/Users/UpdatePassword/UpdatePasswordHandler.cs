using FlexPro.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FlexPro.Application.UseCases.Users.UpdatePassword;

public class UpdatePasswordHandler(UserManager<ApplicationUser>  userManager) : IRequestHandler<UpdatePasswordCommand, string?>
{
    public async Task<string?> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
    {
        var user  = await userManager.FindByNameAsync(request.Dto.Username);
        IdentityResult? result = null;
        if (user != null)
        {
            var passwordValid = await userManager.CheckPasswordAsync(user, request.Dto.Password);
            if (passwordValid && request.Dto.Password != request.Dto.NewPassword)
            {
                result = await userManager.ChangePasswordAsync(user, request.Dto.Password, request.Dto.NewPassword);
            }
        }

        if (result != null && result.Succeeded)
        {
            return "Password updated";
        }

        return "Password could not be updated";
    }
}