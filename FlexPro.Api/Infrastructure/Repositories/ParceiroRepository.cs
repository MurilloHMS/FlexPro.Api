using FlexPro.Api.Application.Interfaces;
using FlexPro.Api.Domain.Entities;
using FlexPro.Api.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace FlexPro.Api.Infrastructure.Repositories;

public class ParceiroRepository : Repository<Parceiro>, IParceiroRepository 
{
    public ParceiroRepository(AppDbContext context) : base(context) { }

    public async Task<Parceiro> GetByNameAsync(string nome)
    {
        Parceiro parceiro = await _dbSet.FirstOrDefaultAsync(x => x.Nome == nome);
        return parceiro;
    }

    public async Task IncludeParceiroByRangeAsync(List<Parceiro> parceiros)
    {
        await _dbSet.AddRangeAsync(parceiros);
        await _context.SaveChangesAsync();
    }
}