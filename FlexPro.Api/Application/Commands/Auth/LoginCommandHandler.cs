using FlexPro.Api.Application.DTOs;
using FlexPro.Api.Application.Interfaces;
using FlexPro.Api.Infrastructure.Persistance;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FlexPro.Api.Application.Commands.Auth
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtTokenGenerator _jwt;

        public LoginCommandHandler(UserManager<ApplicationUser> userManager, IJwtTokenGenerator jwt)
        {
            _userManager = userManager;
            _jwt = jwt;
        }

        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if(user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
                throw new UnauthorizedAccessException("Invalid username or password");

            return _jwt.GenerateToken(user);
        }
    }
}
