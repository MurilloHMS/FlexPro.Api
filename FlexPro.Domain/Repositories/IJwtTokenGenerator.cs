namespace FlexPro.Domain.Repositories
{
    public interface IJwtTokenGenerator<T> where T : class
    {
        Task<string> GenerateToken(T user);
    }
}
