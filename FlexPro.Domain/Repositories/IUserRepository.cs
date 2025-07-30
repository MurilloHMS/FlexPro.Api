using FlexPro.Domain.Entities;

namespace FlexPro.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<LoginModel> GetByUsernameAsync(string username);
        Task AddAsync(LoginModel user);
    }
}
