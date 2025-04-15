using FlexPro.Api.Domain.Entities;
using FlexPro.Api.Infrastructure.Persistance;

namespace FlexPro.Api.Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        Task<string> GenerateToken(ApplicationUser user);
    }
}
