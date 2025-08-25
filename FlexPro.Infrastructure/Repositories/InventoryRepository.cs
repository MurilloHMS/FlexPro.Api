using FlexPro.Domain.Entities;
using FlexPro.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FlexPro.Infrastructure.Repositories;

public class InventoryRepository(AppDbContext context)
{
    public async Task InsertMovementAsync(InventoryMovement inventoryMovement)
    {
        if(inventoryMovement.InventoryProductId <= 0)
            throw new ArgumentException("O movimento deve estar vinculado ao produto");
        
        await context.InventoryMovement.AddAsync(inventoryMovement);
        await context.SaveChangesAsync();
    }

    public async Task<List<InventoryMovement>> GetAllMovements() =>  await context.InventoryMovement.ToListAsync();
    public async Task<List<InventoryProducts>> GetAllProducts() => await context.InventoryProducts.ToListAsync();

    public async Task<List<InventoryProducts>> GetAllAsync() =>
        await context.InventoryProducts.Include(m => m.Movements).ToListAsync();
    
    public async Task DeleteMovement(int id)
    {
        InventoryMovement? movement = await context.InventoryMovement.FindAsync(id);
        if (movement != null)
        {
            context.InventoryMovement.Remove(movement);
            await context.SaveChangesAsync();
        }
    }

}