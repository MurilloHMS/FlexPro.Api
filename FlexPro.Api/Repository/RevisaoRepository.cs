using FlexPro.Api.Data;
using FlexPro.Api.Interfaces;
using FlexPro.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace FlexPro.Api.Repository
{
    public class RevisaoRepository : IRevisaoRepository
    {
        private readonly AppDbContext _context;

        public RevisaoRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Revisao>> GetAll()
        {
            var revisao = await _context.Revisao.ToListAsync();
            return revisao ?? Enumerable.Empty<Revisao>();
        }

        public async Task<Revisao> GetById(int id)
        {
            var revisao = await _context.Revisao.FirstOrDefaultAsync(x => x.Id == id);
            return revisao ?? null;
        }

        public async Task<Revisao> GetByName(string name)
        {
            var revisao = await _context.Revisao.FirstOrDefaultAsync(x => x.Motorista == name);
            return revisao ?? null;
        }

        public async Task UpdateOrInsert(Revisao revisao)
        {
            var revisaoFounded = await _context.Revisao.FirstOrDefaultAsync(x => x.Id == revisao.Id);
            if (revisaoFounded != null)
            {
                _context.Entry(revisaoFounded).CurrentValues.SetValues(revisao);
            }
            else
            {
                _context.Revisao.Add(revisao);
            }

            await _context.SaveChangesAsync();
        }
        public async Task Delete(Revisao revisao)
        {
            _context.Revisao.Remove(revisao);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            var revisao = await _context.Revisao.FirstOrDefaultAsync( x => x.Id == id);
            if (revisao != null)
            {
                _context.Remove(revisao);
                await _context.SaveChangesAsync();
            }
        }
    }
}
