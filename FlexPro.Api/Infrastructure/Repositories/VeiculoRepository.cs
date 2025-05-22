using FlexPro.Api.Infrastructure.Persistance;
using FlexPro.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using FlexPro.Api.Application.Interfaces;

namespace FlexPro.Api.Infrastructure.Repositories
{
    public class VeiculoRepository : Repository<Veiculo>, IVeiculoRepository
    {
        public VeiculoRepository(AppDbContext context) : base(context) { }

        public async Task<Veiculo> GetByNameAsync(string name)
        {
            var vehicle = await _dbSet.FirstOrDefaultAsync(x => x.Nome == name);
            return vehicle ?? null;
        }

        public async Task UpdateOrInsert(Veiculo vehicle)
        {
            var vehicleFounded = await _dbSet.FirstOrDefaultAsync(x => x.Id == vehicle.Id);
            if (vehicleFounded != null)
            {
                _context.Entry(vehicleFounded).CurrentValues.SetValues(vehicle);
            }
            else
            {
                _dbSet.Add(vehicle);
            }
            await _context.SaveChangesAsync();
        }
        
    }
}
