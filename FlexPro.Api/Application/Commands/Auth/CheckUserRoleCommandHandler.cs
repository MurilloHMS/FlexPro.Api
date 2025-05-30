﻿using FlexPro.Api.Application.DTOs.Auth;
using FlexPro.Api.Infrastructure.Persistance;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Org.BouncyCastle.Crypto;

namespace FlexPro.Api.Application.Commands.Auth
{
    public class CheckUserRoleCommandHandler : IRequestHandler<CheckUserRoleCommand, List<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public CheckUserRoleCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<string>> Handle(CheckUserRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Dto.Username);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }
    }
}
