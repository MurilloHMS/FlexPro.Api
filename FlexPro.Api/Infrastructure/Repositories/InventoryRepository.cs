using FlexPro.Api.Application.Interfaces;
using FlexPro.Api.Domain.Entities;
using FlexPro.Api.Infrastructure.Persistance;

namespace FlexPro.Api.Infrastructure.Repositories;

public class InventoryRepository : Repository<InventoryMovement>
{
    public InventoryRepository(AppDbContext context) : base(context) { }
    
}