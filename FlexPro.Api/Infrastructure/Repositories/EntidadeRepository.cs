using FlexPro.Api.Application.Interfaces;
using FlexPro.Api.Domain.Entities;
using FlexPro.Api.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace FlexPro.Api.Infrastructure.Repositories
{
    public class EntidadeRepository : IEntidadeRepository
    {
        private readonly AppDbContext _context;

        public EntidadeRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task Delete(Entidade entidade)
        {
            _context.Entidade.Remove(entidade);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            var entidade = await _context.Entidade.FirstOrDefaultAsync(x => x.Id == id);
            if (entidade == null)
            {
                _context.Entidade.Remove(entidade);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Entidade>> GetAll()
        {
            var entities = await _context.Entidade.ToListAsync();
            return entities ?? Enumerable.Empty<Entidade>();
        }

        public async Task<Entidade> GetById(int id)
        {
            var entidade = await _context.Entidade.FirstOrDefaultAsync(x => x.Id == id);
            return entidade ?? null;
        }

        public async Task SaveOrUpdate(Entidade entidade)
        {
            var entityFounded = _context.Entidade.FirstOrDefault(e => e.Id == entidade.Id);
            if (entityFounded != null)
            {
                _context.Entry(entityFounded).CurrentValues.SetValues(entidade);
            }
            else
            {
                _context.Entidade.Add(entidade);
            }

            await _context.SaveChangesAsync();
        }
    }
}
