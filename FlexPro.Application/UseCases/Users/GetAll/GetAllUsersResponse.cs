using FlexPro.Application.DTOs.Auth;
using FlexPro.Domain.Entities;

namespace FlexPro.Application.UseCases.Users.GetAll;

public sealed record GetAllUsersResponse(IEnumerable<UserDto> Usuarios);