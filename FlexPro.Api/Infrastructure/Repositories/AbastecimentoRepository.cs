﻿using ClosedXML.Excel;
using FlexPro.Api.Application.Interfaces;
using FlexPro.Api.Domain.Entities;
using FlexPro.Api.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Text;

namespace FlexPro.Api.Infrastructure.Repositories
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
            var fuelSupply = await _context.Abastecimento.Where(a => a.DataDoAbastecimento.ToUniversalTime().Date >= initial && a.DataDoAbastecimento.ToUniversalTime().Date <= finish).ToListAsync();
            return fuelSupply;
        }
    }
}
