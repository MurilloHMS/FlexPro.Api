﻿using FlexPro.Api.Infrastructure.Persistance;
using FlexPro.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using FlexPro.Api.Application.Interfaces;

namespace FlexPro.Api.Infrastructure.Repositories
{
    public class VeiculoRepository : IVeiculoRepository
    {
        private readonly AppDbContext _context;

        public VeiculoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Veiculo>> GetAll()
        {
            var vehicle = await _context.Veiculo.ToListAsync();
            return vehicle ?? Enumerable.Empty<Veiculo>();
        }

        public async Task<Veiculo> GetById(int id)
        {
            var vehicle = await _context.Veiculo.FirstOrDefaultAsync(x => x.Id.Equals(id));
            return vehicle ?? null;
        }

        public async Task<Veiculo> GetByName(string name)
        {
            var vehicle = await _context.Veiculo.FirstOrDefaultAsync(x => x.Nome == name);
            return vehicle ?? null;
        }

        public async Task UpdateOrInsert(Veiculo vehicle)
        {
            var vehicleFounded = await _context.Veiculo.FirstOrDefaultAsync(x => x.Id == vehicle.Id);
            if (vehicleFounded != null)
            {
                _context.Entry(vehicleFounded).CurrentValues.SetValues(vehicle);
            }
            else
            {
                _context.Veiculo.Add(vehicle);
            }
            await _context.SaveChangesAsync();
        }
        public async Task Delete(Veiculo vehicle)
        {
            _context.Veiculo.Remove(vehicle);
            await _context.SaveChangesAsync();
        }
    }
}
