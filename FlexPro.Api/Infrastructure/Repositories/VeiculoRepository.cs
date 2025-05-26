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
        
    }
}
