using FlexPro.Api.Domain.Entities;

namespace FlexPro.Api.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<LoginModel> GetByUsernameAsync(string username);
        Task AddAsync(LoginModel user);
    }
}
