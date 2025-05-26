using System.Linq.Expressions;

namespace FlexPro.Api.Application.Interfaces;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task DeleteAsync(T entity);
    Task InsertOrUpdateAsync(T entity, Expression<Func<T, bool>>? predicate = null);
}