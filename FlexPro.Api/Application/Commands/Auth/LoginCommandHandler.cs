using System.Security.Claims;
using FlexPro.Api.Application.DTOs;
using FlexPro.Api.Application.Interfaces;
using FlexPro.Api.Infrastructure.Persistance;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

namespace FlexPro.Api.Application.Commands.Auth
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtTokenGenerator _jwt;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginCommandHandler(UserManager<ApplicationUser> userManager, IJwtTokenGenerator jwt, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _jwt = jwt;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
                return null;

            
            var token = await _jwt.GenerateToken(user);
            return token;
        }
    }
}
