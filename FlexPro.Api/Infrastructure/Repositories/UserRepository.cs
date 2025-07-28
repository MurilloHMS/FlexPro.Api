using FlexPro.Api.Application.Interfaces;
using FlexPro.Api.Domain.Entities;

namespace FlexPro.Api.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly List<LoginModel> _users = new();

        public Task<LoginModel> GetByUsernameAsync(string username)
            => Task.FromResult(_users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)))!;

        public Task AddAsync(LoginModel user)
        {
            _users.Add(user);
            return Task.CompletedTask;
        }
    }
}
