using FlexPro.Api.Domain.Entities;
using FlexPro.Api.Infrastructure.Persistance;

namespace FlexPro.Api.Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser user);
    }
}
