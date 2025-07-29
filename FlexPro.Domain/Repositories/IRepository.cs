using System.Linq.Expressions;
using FlexPro.Domain.Abstractions;

namespace FlexPro.Domain.Repositories;

public interface IRepository<T> where T : Entity
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task DeleteAsync(T entity);
    Task InsertOrUpdateAsync(T entity, Expression<Func<T, bool>>? predicate = null);
}