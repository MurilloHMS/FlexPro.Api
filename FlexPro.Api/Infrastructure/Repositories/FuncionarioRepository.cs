using FlexPro.Api.Application.Interfaces;
using FlexPro.Api.Domain.Entities;
using FlexPro.Api.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace FlexPro.Api.Infrastructure.Repositories
{
    public class FuncionarioRepository : Repository<Funcionario>,IFuncionarioRepository
    {
        public FuncionarioRepository(AppDbContext context) : base(context) { }

        public async Task DeleteById(int id)
        {
            var funcionario = _dbSet.FirstOrDefault(f => f.Id == id);
            if (funcionario != null)
            {
                _context.Funcionarios.Remove(funcionario);
                await _context.SaveChangesAsync();
            }
        }
        public async Task SaveOrUpdate(Funcionario funcionario)
        {
            var employeeFound = await _dbSet.FirstOrDefaultAsync(x => x.Id == funcionario.Id);
            if (employeeFound != null)
            {
                _context.Entry(employeeFound).CurrentValues.SetValues(funcionario);
            }
            else
            {
                _dbSet.Add(funcionario);
            }

            await _context.SaveChangesAsync();
        }
    }
}
