using FlexPro.Application.DTOs.Auth;
using FlexPro.Domain.Entities;
using FlexPro.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FlexPro.Application.UseCases.Auth;

public sealed class AuthenticateLoginHandler(
    IJwtTokenGenerator<ApplicationUser> jwtTokenGenerator,
    UserManager<ApplicationUser> userManager) 
    : IRequestHandler<AuthenticateLoginCommand, LoginResponse?>
{
    public async Task<LoginResponse?> Handle(AuthenticateLoginCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.LoginRequest.Username);
        if (user == null || !await userManager.CheckPasswordAsync(user, request.LoginRequest.Password))
            return null;

        var token = await jwtTokenGenerator.GenerateToken(user);
        LoginResponse response  = token;
        return response;
    }
}