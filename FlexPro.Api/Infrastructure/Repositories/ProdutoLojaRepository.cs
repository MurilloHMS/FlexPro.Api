using FlexPro.Api.Application.Interfaces;
using FlexPro.Api.Domain.Entities;
using FlexPro.Api.Infrastructure.Persistance;

namespace FlexPro.Api.Infrastructure.Repositories;

public class ProdutoLojaRepository : Repository<ProdutoLoja>, IProdutoLojaRepository
{
    public ProdutoLojaRepository(AppDbContext context) : base(context) { }
    
    
}