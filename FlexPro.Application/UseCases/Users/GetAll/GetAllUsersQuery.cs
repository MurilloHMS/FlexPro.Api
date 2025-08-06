using FlexPro.Domain.Abstractions;
using MediatR;

namespace FlexPro.Application.UseCases.Users.GetAll;

public sealed record GetAllUsersQuery : IRequest<Result<GetAllUsersResponse>>;