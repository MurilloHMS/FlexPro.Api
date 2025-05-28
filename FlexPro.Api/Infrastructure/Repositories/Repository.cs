using System.Linq.Expressions;
using FlexPro.Api.Application.Interfaces;
using FlexPro.Api.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace FlexPro.Api.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }
    
    public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

    public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

    public async Task DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }
    
    public async Task InsertOrUpdateAsync(T entity, Expression<Func<T, bool>>? predicate = null)
    {
        T? entityFounded = null;
        if (predicate != null)
        {
            entityFounded = await _dbSet.FirstOrDefaultAsync(predicate);
        }
        else
        {
            var entry = _context.Entry(entity);
            var key = _context.Model.FindEntityType(typeof(T))?.FindPrimaryKey();

            if (key != null)
            {
                var keyProperties = key.Properties.First();
                var keyValue = entry.Property(keyProperties.Name).CurrentValue;

                if (keyValue != null && !keyValue.Equals(GetDefault(keyProperties.ClrType)))
                {
                    entityFounded = await _dbSet.FindAsync(keyValue);
                }
            }
        }

        if (entityFounded != null)
        {
            _context.Entry(entityFounded).CurrentValues.SetValues(entity);
        }
        else
        {
            await _dbSet.AddAsync(entity);
        }
        
        await _context.SaveChangesAsync();
    }
    
    private static object? GetDefault(Type type)
    {
        return type.IsValueType ? Activator.CreateInstance(type) : null;
    }
}