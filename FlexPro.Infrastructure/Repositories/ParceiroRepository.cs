using FlexPro.Domain.Entities;
using FlexPro.Domain.Repositories;
using FlexPro.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FlexPro.Infrastructure.Repositories;

public class ParceiroRepository(AppDbContext context) : Repository<Parceiro>(context), IParceiroRepository 
{
    private readonly DbSet<Parceiro> _dbSet = context.Set<Parceiro>();

    public async Task<Parceiro> GetByNameAsync(string nome)
    {
        Parceiro parceiro = await _dbSet.FirstOrDefaultAsync(x => x.Nome == nome);
        return parceiro;
    }

    public async Task IncludeParceiroByRangeAsync(List<Parceiro> parceiros)
    {
        var codigoParceiros = parceiros.Select(x => x.CodigoSistema).ToList();
        
        var parceirosExistentes = await _dbSet.Where(p => codigoParceiros.Contains(p.CodigoSistema)).ToListAsync();

        foreach (var parceiro in parceiros)
        {
            var parceiroExistente = parceirosExistentes.FirstOrDefault(p => p.CodigoSistema == parceiro.CodigoSistema);

            if (parceiroExistente != null)
            {
                parceiroExistente.Nome = parceiro.Nome;
                parceiroExistente.Email = parceiro.Email;
                parceiroExistente.RazaoSocial = parceiro.RazaoSocial;
            }
            else
            {
                await _dbSet.AddAsync(parceiro);
            }
        }
        
        await context.SaveChangesAsync();
    }
}