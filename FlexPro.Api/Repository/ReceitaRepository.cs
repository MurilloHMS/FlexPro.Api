using FlexPro.Api.Data;
using FlexPro.Api.Interfaces;
using FlexPro.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace FlexPro.Api.Repository
{
    public class ReceitaRepository : IReceitaRepository
    {
        private readonly AppDbContext _context;
        public ReceitaRepository(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<Receita>> GetAll()
        {
            var receita = await _context.Receita.ToListAsync();
            return receita ?? Enumerable.Empty<Receita>();
        }

        public async Task<Receita> GetById(int id)
        {
            var receita = await _context.Receita.FirstOrDefaultAsync(x => x.Id == id);
            return receita ?? null;
        }

        public async Task SaveOrUpdate(Receita receita)
        {
            var receiptFounded = await _context.Receita.FirstOrDefaultAsync(x => x.Id == receita.Id);
            if (receiptFounded != null)
            {
                _context.Entry(receiptFounded).CurrentValues.SetValues(receita);
            }
            else
            {
                await _context.Receita.AddAsync(receita);
            }
            await _context.SaveChangesAsync();
        }
        public async Task Delete(Receita receita)
        {
            _context.Receita.Remove(receita);
            await _context.SaveChangesAsync();
        }
    }
}
