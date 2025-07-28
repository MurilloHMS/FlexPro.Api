using FlexPro.Api.Application.Interfaces;
using FlexPro.Api.Domain.Entities;
using FlexPro.Api.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace FlexPro.Api.Infrastructure.Repositories
{
    public class PrestadorDeServicoRepository : Repository<PrestadorDeServico>, IPrestadorDeServicoRepository
    {
        public PrestadorDeServicoRepository(AppDbContext context) : base(context) { }

        public async Task DeleteById(int id)
        {
            var entidade = await _context.PrestadorDeServico.FirstOrDefaultAsync(x => x.Id == id);
            if (entidade != null)
            {
                _context.PrestadorDeServico.Remove(entidade);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<PrestadorDeServico>> GetAllAsync()
        {
            var entities = await _context.PrestadorDeServico.ToListAsync();
            return entities ?? Enumerable.Empty<PrestadorDeServico>();
        }

        public async Task<PrestadorDeServico?> GetByIdAsync(int id)
        {
            var entidade = await _context.PrestadorDeServico.FirstOrDefaultAsync(x => x.Id == id);
            return entidade ?? null;
        }
    }
}
