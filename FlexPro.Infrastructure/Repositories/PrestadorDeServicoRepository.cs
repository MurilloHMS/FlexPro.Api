using FlexPro.Domain.Entities;
using FlexPro.Domain.Repositories;
using FlexPro.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FlexPro.Infrastructure.Repositories;

public class PrestadorDeServicoRepository(AppDbContext context)
    : Repository<PrestadorDeServico>(context), IPrestadorDeServicoRepository
{
    public async Task DeleteById(int id)
    {
        var entidade = await context.PrestadorDeServico.FirstOrDefaultAsync(x => x.Id == id);
        if (entidade != null)
        {
            context.PrestadorDeServico.Remove(entidade);
            await context.SaveChangesAsync();
        }
    }
}