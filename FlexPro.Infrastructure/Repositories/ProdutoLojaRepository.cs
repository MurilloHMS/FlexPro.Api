using FlexPro.Domain.Entities;
using FlexPro.Domain.Repositories;
using FlexPro.Infrastructure.Data;

namespace FlexPro.Infrastructure.Repositories;

public class ProdutoLojaRepository : Repository<ProdutoLoja>, IProdutoLojaRepository
{
    public ProdutoLojaRepository(AppDbContext context) : base(context) { }
    
    
}