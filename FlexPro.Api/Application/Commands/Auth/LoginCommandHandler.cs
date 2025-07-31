using FlexPro.Domain.Repositories;
using FlexPro.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FlexPro.Api.Application.Commands.Auth
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtTokenGenerator<ApplicationUser> _jwt;

        public LoginCommandHandler(UserManager<ApplicationUser> userManager, IJwtTokenGenerator<ApplicationUser> jwt)
        {
            _userManager = userManager;
            _jwt = jwt;
        }


        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
                return null!;


            var token = await _jwt.GenerateToken(user);
            return token;
        }
    }
}