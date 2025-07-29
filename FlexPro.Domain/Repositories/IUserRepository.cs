using FlexPro.Domain.Entities;

namespace FlexPro.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task<LoginModel> GetByUsernameAsync(string username);
        Task AddAsync(LoginModel user);
    }
}
