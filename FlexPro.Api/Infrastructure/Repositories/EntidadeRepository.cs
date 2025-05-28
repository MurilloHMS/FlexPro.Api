using FlexPro.Api.Application.Interfaces;
using FlexPro.Api.Domain.Entities;
using FlexPro.Api.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace FlexPro.Api.Infrastructure.Repositories
{
    public class EntidadeRepository : Repository<Entidade>, IEntidadeRepository
    {
        public EntidadeRepository(AppDbContext context) : base(context) { }

        public async Task DeleteAsync(Entidade entidade)
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

        public async Task<IEnumerable<Entidade>> GetAllAsync()
        {
            var entities = await _context.Entidade.ToListAsync();
            return entities ?? Enumerable.Empty<Entidade>();
        }

        public async Task<Entidade> GetByIdAsync(int id)
        {
            var entidade = await _context.Entidade.FirstOrDefaultAsync(x => x.Id == id);
            return entidade ?? null;
        }
    }
}
