using FlexPro.Api.Application.Interfaces;
using FlexPro.Api.Domain.Entities;
using FlexPro.Api.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace FlexPro.Api.Infrastructure.Repositories
{
    public class RevisaoRepository : Repository<Revisao>, IRevisaoRepository
    {
        public RevisaoRepository(AppDbContext context) : base(context) { }

        public async Task<Revisao> GetByName(string name)
        {
            var revisao = await _dbSet.FirstOrDefaultAsync(x => x.Motorista == name);
            return revisao ?? null;
        }

        public async Task<IEnumerable<Revisao>> GetByVehicleId(int vehicleId)
        {
            var revisao = await _dbSet.Where(x => x.VeiculoId == vehicleId).ToListAsync();
            return revisao.Any() ? revisao : Enumerable.Empty<Revisao>();
        }

        public async Task UpdateOrInsert(Revisao revisao)
        {
            var revisaoFounded = await _dbSet.FirstOrDefaultAsync(x => x.Id == revisao.Id);
            if (revisaoFounded != null)
            {
                _context.Entry(revisaoFounded).CurrentValues.SetValues(revisao);
            }
            else
            {
                _context.Revisao.Add(revisao);
            }

            await _context.SaveChangesAsync();
        }
        public async Task DeleteById(int id)
        {
            var revisao = await _dbSet.FirstOrDefaultAsync( x => x.Id == id);
            if (revisao != null)
            {
                _context.Remove(revisao);
                await _context.SaveChangesAsync();
            }
        }
    }
}
