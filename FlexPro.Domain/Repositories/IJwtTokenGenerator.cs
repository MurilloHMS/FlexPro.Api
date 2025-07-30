namespace FlexPro.Domain.Repositories
{
    public interface IJwtTokenGenerator<TUser>
    {
        Task<string> GenerateToken(TUser user);
    }
}
