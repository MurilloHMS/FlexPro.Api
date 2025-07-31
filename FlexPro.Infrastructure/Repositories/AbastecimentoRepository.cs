using FlexPro.Domain.Entities;
using FlexPro.Domain.Repositories;
using FlexPro.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FlexPro.Infrastructure.Repositories;

public class AbastecimentoRepository : IAbastecimentoRepository
{
    private readonly AppDbContext _context;

    public AbastecimentoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddFuelSupply(Abastecimento fuelSupply)
    {
        await _context.Abastecimento.AddAsync(fuelSupply);
        await _context.SaveChangesAsync();
    }

    public async Task AddRangeFuelSupply(List<Abastecimento> fuelSupplies)
    {
        await _context.Abastecimento.AddRangeAsync(fuelSupplies);
        await _context.SaveChangesAsync();
    }

    public Task RemoveFuelSupply(int fuelSupplyId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Abastecimento>> GetFuelSupply()
    {
        var fuelSupply = await _context.Abastecimento.ToListAsync();
        return fuelSupply;
    }

    public async Task<List<Abastecimento>> GetFuelSupplyByDate(DateTime initial, DateTime finish)
    {
        var fuelSupply = await _context.Abastecimento.Where(a =>
            a.DataDoAbastecimento.ToUniversalTime().Date >= initial &&
            a.DataDoAbastecimento.ToUniversalTime().Date <= finish).ToListAsync();
        return fuelSupply;
    }
}