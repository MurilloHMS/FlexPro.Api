using FlexPro.Api.Application.Interfaces;
using FlexPro.Api.Domain.Entities;
using FlexPro.Api.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace FlexPro.Api.Infrastructure.Repositories
{
    public class FuncionarioRepository : IFuncionarioRepository
    {
        private readonly AppDbContext _context;
        public FuncionarioRepository(AppDbContext context) 
        {
            _context = context;
        }
        public async Task Delete(Funcionario funcionario)
        {
            _context.Funcionarios.Remove(funcionario);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            var funcionario = _context.Funcionarios.FirstOrDefault(f => f.Id == id);
            if (funcionario != null)
            {
                _context.Funcionarios.Remove(funcionario);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Funcionario>> GetAll()
        {
            var employee = await _context.Funcionarios.ToListAsync();
            return employee ?? Enumerable.Empty<Funcionario>();
        }

        public async Task<Funcionario> GetById(int id)
        {
            var employee = await _context.Funcionarios.FirstOrDefaultAsync(f => id == f.Id);
            return employee ?? null;
        }

        public async Task SaveOrUpdate(Funcionario funcionario)
        {
            var employeeFound = await _context.Funcionarios.FirstOrDefaultAsync(x => x.Id == funcionario.Id);
            if (employeeFound != null)
            {
                _context.Entry(employeeFound).CurrentValues.SetValues(funcionario);
            }
            else
            {
                _context.Funcionarios.Add(funcionario);
            }

            await _context.SaveChangesAsync();
        }
    }
}
