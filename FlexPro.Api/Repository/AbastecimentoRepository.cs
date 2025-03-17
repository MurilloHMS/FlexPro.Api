using ClosedXML.Excel;
using FlexPro.Api.Data;
using FlexPro.Api.Interfaces;
using FlexPro.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Text;

namespace FlexPro.Api.Repository
{
    public class AbastecimentoRepository : IAbastecimentoRepository
    {
        private AppDbContext _context;
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

        public Task ExportData(List<Abastecimento> fuelSupplies)
        {
            throw new NotImplementedException();
        }

        public Task ImportData()
        {
            throw new NotImplementedException();
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
            var fuelSupply = await _context.Abastecimento.Where(a => a.DataDoAbastecimento >= initial && a.DataDoAbastecimento <= finish).ToListAsync();
            return fuelSupply;
        }
    }
}
