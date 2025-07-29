using FlexPro.Domain.Entities;
using FlexPro.Infrastructure.Data;

namespace FlexPro.Infrastructure.Repositories;

public class InventoryRepository : Repository<InventoryMovement>
{
    public InventoryRepository(AppDbContext context) : base(context) { }
    
}