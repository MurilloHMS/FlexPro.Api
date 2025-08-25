using FlexPro.Application.DTOs.Auth;
using FlexPro.Domain.Entities;
using FlexPro.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Email = Flexpro.Application.ValueObjects.Email;

namespace FlexPro.Application.UseCases.Auth;

public sealed class AuthenticateLoginHandler(
    IJwtTokenGenerator<ApplicationUser> jwtTokenGenerator,
    UserManager<ApplicationUser> userManager) 
    : IRequestHandler<AuthenticateLoginCommand, LoginResponse?>
{
    public async Task<LoginResponse?> Handle(AuthenticateLoginCommand request, CancellationToken cancellationToken)
    {
        ApplicationUser? user;

        var login = request.LoginRequest.Username;

        if (Flexpro.Application.ValueObjects.Email.IsValid(login))
        {
            var email = new Flexpro.Application.ValueObjects.Email(login);
            user = await userManager.FindByEmailAsync(email);
        }
        else
        {
            user = await userManager.FindByNameAsync(login);
        }
        
        if (user == null || !await userManager.CheckPasswordAsync(user, request.LoginRequest.Password))
            return null;

        var token = await jwtTokenGenerator.GenerateToken(user);
        LoginResponse response  = token;
        return response;
    }
}