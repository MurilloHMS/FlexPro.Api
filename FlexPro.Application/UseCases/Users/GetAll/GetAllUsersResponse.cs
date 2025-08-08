using FlexPro.Application.DTOs.Auth;

namespace FlexPro.Application.UseCases.Users.GetAll;

public sealed record GetAllUsersResponse(IEnumerable<UserDto> UserResponse);