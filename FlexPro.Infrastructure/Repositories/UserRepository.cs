using FlexPro.Domain.Entities;
using FlexPro.Domain.Repositories;

namespace FlexPro.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly List<LoginModel> _users = new();

    public Task<LoginModel> GetByUsernameAsync(string username)
    {
        return Task.FromResult(_users.FirstOrDefault(u =>
            u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)))!;
    }

    public Task AddAsync(LoginModel user)
    {
        _users.Add(user);
        return Task.CompletedTask;
    }
}