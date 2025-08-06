using FlexPro.Application.DTOs.Auth;
using FlexPro.Domain.Abstractions;
using FlexPro.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FlexPro.Application.UseCases.Users.GetAll;

public sealed class GetAllUsersHandler(UserManager<ApplicationUser> userManager) : IRequestHandler<GetAllUsersQuery, Result<GetAllUsersResponse>>
{
    public async Task<Result<GetAllUsersResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await userManager.Users.ToListAsync();
        var userDtos = new List<UserDto>();

        foreach (var user in users)
        {
            var roles =  await userManager.GetRolesAsync(user);
            userDtos.Add(new  UserDto
            {
                Id = user.Id,
                Email = user.Email!,
                Roles = roles.ToList()
            });
        }

        return userDtos.Any()
            ? Result.Success(new GetAllUsersResponse(userDtos))
            : Result.Failure<GetAllUsersResponse>(new Error("404", "Users not found"));
    }
}